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
                // foreach (var p in r.Where(property => property.UnderlyingName == "FormattedMessage"))
                // {
                //     p.Ignored = true;
                //     found = true;
                // }
                //
                // if (!found)
                // {
                //     throw new Exception("Beep");
                // }
            }

            return r;
        }

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        //private readonly DefaultContractResolver contractResolver { get { }};

        public ContractResolver()
        {
            //contractResolver = new DefaultContractResolver();
            // LogEventInfoContract =
            //     new LogEventInfoContract(/*contractResolver.ResolveContract(typeof(LogEventInfo))*/);
            //var _wpf = base.ResolveContract(typeof(WpfLogEventInfo));
            //Logger.Debug($"default contract is {_wpf}");
            //WpfLogEventInfoContract = new WpfLogEventInfoContract(/*_wpf*/);
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

        private JsonContract _ResolveContract(Type type)
        {
            Logger.Debug($"resolve contract for {type}");
            if (type == typeof(LogLevel))
            {
                //     return new JsonObjectContract()
                //     {
                //         CreatorParameters = { }
                //         DefaultCreator =  
                //     }
            }
            /*
            if (typeof(WpfLogEventInfo).IsAssignableFrom(type))
            {
                Logger.Debug($"returning {WpfLogEventInfoContract}");
                return WpfLogEventInfoContract;
            }
            else */ if (typeof(LogEventInfo).IsAssignableFrom(type))
            {
                Logger.Debug($"returning {LogEventInfoContract}");
                return LogEventInfoContract;
            }

            return null;//cotractResolver.ResolveContract(type);
        }

        public JsonContract LogEventInfoContract { get; set; }

        public JsonContract WpfLogEventInfoContract { get; set; }
    }
}