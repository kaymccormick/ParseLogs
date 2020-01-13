using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NLog;
using ObjectsComparer;

namespace LogAdjunct
{
    public class ContractResolver : DefaultContractResolver
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        protected override JsonObjectContract CreateObjectContract(Type objectType)
        {
            Logger.Debug($"{nameof(CreateObjectContract)} ( {objectType} )");
            
            return base.CreateObjectContract(objectType);
        }

        protected override JsonConverter ResolveContractConverter(Type objectType)
        {
            var r = base.ResolveContractConverter(objectType);
            Logger.Debug($"{nameof(ResolveContractConverter)} ({objectType})");
            return r;
        }

        protected override JsonContract CreateContract(Type objectType)
        {
            Logger.Info($"{nameof(CreateContract)}: {objectType}");
            var baseR = base.CreateContract(objectType);
            JsonContract result = null;
            JsonObjectContract objContract = null;
            if (objectType == typeof(LogLevel))
            {
                result = objContract = new JsonObjectContract(objectType)
                {
                    MemberSerialization = MemberSerialization.OptIn
                };
            }
            else if (objectType == typeof(LogEventInfo))
            {
                result = objContract = new JsonObjectContract(objectType)
                {
                    MemberSerialization = MemberSerialization.OptOut,
                };
            }

            if (result != null)
            {
                Logger.Debug($"Constructed custom contract for {objectType}");
            }
            if (objContract != null)
            {
                var ro = baseR as JsonObjectContract;
                if(ro != null)
                {
                    Logger.Debug("performing compare");
                    var x = new ObjectsComparer.Comparer<JsonObjectContract>();
                    var d = x.CalculateDifferences(ro, objContract);
                    foreach(var diff in d)
                    {
                        Logger.Debug($"{diff}");
                    }
                }
                Logger.Debug($"ObjectContract has MemberSerialization of {objContract.MemberSerialization}");
            }

            if (result == null)
            {
                Logger.Debug($"calling base method {nameof(CreateContract)}");

            }

            return result;
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            Logger.Debug($"{nameof(CreateProperties)} ({type}, {memberSerialization}");
            var r = base.CreateProperties(type, memberSerialization);
            // if (type == typeof(LogLevel))
            // {
            //     var t = this.ResolveContract(type) as JsonObjectContract;
            //     Logger.Debug(t.GetHashCode().ToString);
            //
            // }
            if (type == typeof(LogLevel))
            {
                foreach (var q in r)
                {
                    Logger.Debug($"eep {q.UnderlyingName}");
                    if (q.UnderlyingName == "Level")
                    {
                        //q.converte
                        q.ValueProvider = new LogLevelProvider();
                    }
                }

                return r;
            }

            if (typeof(LogEventInfo).IsAssignableFrom(type))
            {
                bool found = false;
                var r2 = r.Where(p => p.UnderlyingName != "FormattedMessage").ToList();
                found = r2.Count == r.Count - 1;
                foreach (var q in r2)
                {
                    if (q.UnderlyingName == "Level")
                    {
                        q.ValueProvider = new LogLevelProvider();
                    }
                }

                Debug.Assert(found);
                return r2;
            }

            return r;
        }

        public override JsonContract ResolveContract(Type type)
        {
            var r = base.ResolveContract(type);
            if (type == typeof(LogLevel))
            {
                var q = r as JsonObjectContract;
                //q.MemberSerialization = MemberSerialization.OptIn;
                return q;
            }

            //Logger.Debug(r.GetType() + " " + type.FullName);
            return r;
        }
    }
}