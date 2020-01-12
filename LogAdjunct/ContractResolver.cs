using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NLog;

namespace LogAdjunct
{
    public class ContractResolver : DefaultContractResolver
    {
        protected override JsonConverter ResolveContractConverter(Type objectType)
        {
            var r =  base.ResolveContractConverter(objectType);
            
            //Logger.Debug($"{objectType} ; {r.//GetType()}");
            return r;
        }

        protected override JsonContract CreateContract(Type objectType)
        {
            if (objectType == typeof(LogLevel))
            {
                return new JsonObjectContract(objectType);
                var q = CreateProperties(objectType, MemberSerialization.OptIn);
                Debug.Assert(q.Any());
            }
            return base.CreateContract(objectType);
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {

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
                foreach(var q in r2)
                {
                    if(q.UnderlyingName == "Level")
                    {
                        q.ValueProvider = new LogLevelProvider();
                    }
                }
                Debug.Assert(found);
                return r2;
            }

            return r;
        }

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public ContractResolver()
        {
        }

        public override JsonContract ResolveContract(Type type)
        {
            var r = base.ResolveContract(type);
            if(type == typeof(LogLevel))
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