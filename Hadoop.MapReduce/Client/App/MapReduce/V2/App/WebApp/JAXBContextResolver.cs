using System;
using System.Collections.Generic;
using Com.Sun.Jersey.Api.Json;
using Javax.WS.RS.Ext;
using Javax.Xml.Bind;
using Org.Apache.Hadoop.Mapreduce.V2.App.Webapp.Dao;
using Org.Apache.Hadoop.Yarn.Webapp;
using Sharpen;

namespace Org.Apache.Hadoop.Mapreduce.V2.App.Webapp
{
	public class JAXBContextResolver : ContextResolver<JAXBContext>
	{
		private JAXBContext context;

		private readonly ICollection<Type> types;

		private readonly Type[] cTypes = new Type[] { typeof(AMAttemptInfo), typeof(AMAttemptsInfo
			), typeof(AppInfo), typeof(CounterInfo), typeof(JobTaskAttemptCounterInfo), typeof(
			JobTaskCounterInfo), typeof(TaskCounterGroupInfo), typeof(ConfInfo), typeof(JobCounterInfo
			), typeof(TaskCounterInfo), typeof(CounterGroupInfo), typeof(JobInfo), typeof(JobsInfo
			), typeof(ReduceTaskAttemptInfo), typeof(TaskAttemptInfo), typeof(TaskInfo), typeof(
			TasksInfo), typeof(TaskAttemptsInfo), typeof(ConfEntryInfo), typeof(RemoteExceptionData
			) };

		/// <exception cref="System.Exception"/>
		public JAXBContextResolver()
		{
			// you have to specify all the dao classes here
			this.types = new HashSet<Type>(Arrays.AsList(cTypes));
			this.context = new JSONJAXBContext(JSONConfiguration.Natural().RootUnwrapping(false
				).Build(), cTypes);
		}

		public virtual JAXBContext GetContext(Type objectType)
		{
			return (types.Contains(objectType)) ? context : null;
		}
	}
}
