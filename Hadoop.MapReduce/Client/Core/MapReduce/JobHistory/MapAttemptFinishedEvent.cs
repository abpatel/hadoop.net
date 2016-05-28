using System;
using Org.Apache.Avro.Util;
using Org.Apache.Hadoop.Mapred;
using Org.Apache.Hadoop.Mapreduce;
using Sharpen;

namespace Org.Apache.Hadoop.Mapreduce.Jobhistory
{
	/// <summary>Event to record successful completion of a map attempt</summary>
	public class MapAttemptFinishedEvent : HistoryEvent
	{
		private MapAttemptFinished datum = null;

		private TaskAttemptID attemptId;

		private TaskType taskType;

		private string taskStatus;

		private long finishTime;

		private string hostname;

		private string rackName;

		private int port;

		private long mapFinishTime;

		private string state;

		private Counters counters;

		internal int[][] allSplits;

		internal int[] clockSplits;

		internal int[] cpuUsages;

		internal int[] vMemKbytes;

		internal int[] physMemKbytes;

		/// <summary>Create an event for successful completion of map attempts</summary>
		/// <param name="id">Task Attempt ID</param>
		/// <param name="taskType">Type of the task</param>
		/// <param name="taskStatus">Status of the task</param>
		/// <param name="mapFinishTime">Finish time of the map phase</param>
		/// <param name="finishTime">Finish time of the attempt</param>
		/// <param name="hostname">Name of the host where the map executed</param>
		/// <param name="port">RPC port for the tracker host.</param>
		/// <param name="rackName">Name of the rack where the map executed</param>
		/// <param name="state">State string for the attempt</param>
		/// <param name="counters">Counters for the attempt</param>
		/// <param name="allSplits">
		/// the "splits", or a pixelated graph of various
		/// measurable worker node state variables against progress.
		/// Currently there are four; wallclock time, CPU time,
		/// virtual memory and physical memory.
		/// If you have no splits data, code
		/// <see langword="null"/>
		/// for this
		/// parameter.
		/// </param>
		public MapAttemptFinishedEvent(TaskAttemptID id, TaskType taskType, string taskStatus
			, long mapFinishTime, long finishTime, string hostname, int port, string rackName
			, string state, Counters counters, int[][] allSplits)
		{
			this.attemptId = id;
			this.taskType = taskType;
			this.taskStatus = taskStatus;
			this.mapFinishTime = mapFinishTime;
			this.finishTime = finishTime;
			this.hostname = hostname;
			this.rackName = rackName;
			this.port = port;
			this.state = state;
			this.counters = counters;
			this.allSplits = allSplits;
			this.clockSplits = ProgressSplitsBlock.ArrayGetWallclockTime(allSplits);
			this.cpuUsages = ProgressSplitsBlock.ArrayGetCPUTime(allSplits);
			this.vMemKbytes = ProgressSplitsBlock.ArrayGetVMemKbytes(allSplits);
			this.physMemKbytes = ProgressSplitsBlock.ArrayGetPhysMemKbytes(allSplits);
		}

		/// <param name="id">Task Attempt ID</param>
		/// <param name="taskType">Type of the task</param>
		/// <param name="taskStatus">Status of the task</param>
		/// <param name="mapFinishTime">Finish time of the map phase</param>
		/// <param name="finishTime">Finish time of the attempt</param>
		/// <param name="hostname">Name of the host where the map executed</param>
		/// <param name="state">State string for the attempt</param>
		/// <param name="counters">Counters for the attempt</param>
		[System.ObsoleteAttribute(@"please use the constructor with an additional argument, an array of splits arrays instead.  SeeOrg.Apache.Hadoop.Mapred.ProgressSplitsBlock for an explanation of the meaning of that parameter. Create an event for successful completion of map attempts"
			)]
		public MapAttemptFinishedEvent(TaskAttemptID id, TaskType taskType, string taskStatus
			, long mapFinishTime, long finishTime, string hostname, string state, Counters counters
			)
			: this(id, taskType, taskStatus, mapFinishTime, finishTime, hostname, -1, string.Empty
				, state, counters, null)
		{
		}

		internal MapAttemptFinishedEvent()
		{
		}

		public virtual object GetDatum()
		{
			if (datum == null)
			{
				datum = new MapAttemptFinished();
				datum.taskid = new Utf8(attemptId.GetTaskID().ToString());
				datum.attemptId = new Utf8(attemptId.ToString());
				datum.taskType = new Utf8(taskType.ToString());
				datum.taskStatus = new Utf8(taskStatus);
				datum.mapFinishTime = mapFinishTime;
				datum.finishTime = finishTime;
				datum.hostname = new Utf8(hostname);
				datum.port = port;
				if (rackName != null)
				{
					datum.rackname = new Utf8(rackName);
				}
				datum.state = new Utf8(state);
				datum.counters = EventWriter.ToAvro(counters);
				datum.clockSplits = AvroArrayUtils.ToAvro(ProgressSplitsBlock.ArrayGetWallclockTime
					(allSplits));
				datum.cpuUsages = AvroArrayUtils.ToAvro(ProgressSplitsBlock.ArrayGetCPUTime(allSplits
					));
				datum.vMemKbytes = AvroArrayUtils.ToAvro(ProgressSplitsBlock.ArrayGetVMemKbytes(allSplits
					));
				datum.physMemKbytes = AvroArrayUtils.ToAvro(ProgressSplitsBlock.ArrayGetPhysMemKbytes
					(allSplits));
			}
			return datum;
		}

		public virtual void SetDatum(object oDatum)
		{
			this.datum = (MapAttemptFinished)oDatum;
			this.attemptId = TaskAttemptID.ForName(datum.attemptId.ToString());
			this.taskType = TaskType.ValueOf(datum.taskType.ToString());
			this.taskStatus = datum.taskStatus.ToString();
			this.mapFinishTime = datum.mapFinishTime;
			this.finishTime = datum.finishTime;
			this.hostname = datum.hostname.ToString();
			this.rackName = datum.rackname.ToString();
			this.port = datum.port;
			this.state = datum.state.ToString();
			this.counters = EventReader.FromAvro(datum.counters);
			this.clockSplits = AvroArrayUtils.FromAvro(datum.clockSplits);
			this.cpuUsages = AvroArrayUtils.FromAvro(datum.cpuUsages);
			this.vMemKbytes = AvroArrayUtils.FromAvro(datum.vMemKbytes);
			this.physMemKbytes = AvroArrayUtils.FromAvro(datum.physMemKbytes);
		}

		/// <summary>Get the task ID</summary>
		public virtual TaskID GetTaskId()
		{
			return attemptId.GetTaskID();
		}

		/// <summary>Get the attempt id</summary>
		public virtual TaskAttemptID GetAttemptId()
		{
			return attemptId;
		}

		/// <summary>Get the task type</summary>
		public virtual TaskType GetTaskType()
		{
			return taskType;
		}

		/// <summary>Get the task status</summary>
		public virtual string GetTaskStatus()
		{
			return taskStatus.ToString();
		}

		/// <summary>Get the map phase finish time</summary>
		public virtual long GetMapFinishTime()
		{
			return mapFinishTime;
		}

		/// <summary>Get the attempt finish time</summary>
		public virtual long GetFinishTime()
		{
			return finishTime;
		}

		/// <summary>Get the host name</summary>
		public virtual string GetHostname()
		{
			return hostname.ToString();
		}

		/// <summary>Get the tracker rpc port</summary>
		public virtual int GetPort()
		{
			return port;
		}

		/// <summary>Get the rack name</summary>
		public virtual string GetRackName()
		{
			return rackName == null ? null : rackName.ToString();
		}

		/// <summary>Get the state string</summary>
		public virtual string GetState()
		{
			return state.ToString();
		}

		/// <summary>Get the counters</summary>
		internal virtual Counters GetCounters()
		{
			return counters;
		}

		/// <summary>Get the event type</summary>
		public virtual EventType GetEventType()
		{
			return EventType.MapAttemptFinished;
		}

		public virtual int[] GetClockSplits()
		{
			return clockSplits;
		}

		public virtual int[] GetCpuUsages()
		{
			return cpuUsages;
		}

		public virtual int[] GetVMemKbytes()
		{
			return vMemKbytes;
		}

		public virtual int[] GetPhysMemKbytes()
		{
			return physMemKbytes;
		}
	}
}
