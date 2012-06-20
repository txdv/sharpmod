using System;
using System.Collections.Generic;
using SharpMod.Helper;

namespace SharpMod
{
	public static class TaskManager
	{
		class Task
		{
			public bool     Repeat     { get; set; }
			public float    Time       { get; set; }
			public float    AddTime    { get; set; }
			public Delegate Function   { get; set; }
			public object[] Parameters { get; set; }

			public Task()
			{
			}
		}

		private static List<Task> list = new List<Task>();

		#region Join Generic Methods

		public static void Join<T1>(Action<T1> function, T1 arg1)
		{
			Join(function, new object[] { arg1 });
		}

		public static void Join<T1, T2>(Action<T1, T2> function, T1 arg1, T2 arg2)
		{
			Join(function, new object[] { arg1, arg2 });
		}

		public static void Join<T1, T2, T3>(Action<T1, T2, T3> function, T1 arg1, T2 arg2, T3 arg3)
		{
			Join(function, new object[] { arg1, arg2, arg3 });
		}

		public static void Join<T1, T2, T3, T4>(Action<T1, T2, T3, T4> function, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
		{
			Join(function, new object[] { arg1, arg2, arg3, arg4 });
		}

		public static void Join<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> function, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
		{
			Join(function, new object[] { arg1, arg2, arg3, arg4, arg5 });
		}

		public static void Join<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> function, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
		{
			Join(function, new object[] { arg1, arg2, arg3, arg4, arg5, arg6 });
		}

		public static void Join<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> function, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
		{
			Join(function, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7 });
		}

		public static void Join<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> function, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
		{
			Join(function, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8 });
		}

		public static void Join<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> function, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
		{
			Join(function, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9 });
		}

		public static void Join<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> function, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
		{
			Join(function, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10 });
		}

		public static void Join<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> function, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
		{
			Join(function, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11 });
		}

		public static void Join<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> function, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
		{
			Join(function, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12 });
		}

		public static void Join<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> function, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
		{
			Join(function, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13 });
		}

		public static void Join<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> function, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
		{
			Join(function, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14 });
		}

		public static void Join<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> function, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
		{
			Join(function, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15 });
		}

		public static void Join<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> function, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
		{
			Join(function, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16 });
		}

		#endregion

		public static void Join(Delegate function)
		{
			SetTask(function, 0.0f);
		}

		public static void Join(Delegate function, object[] parameters)
		{
			SetTask(function, 0.0f, false, parameters);
		}

		public static void SetTask(Delegate function, float time)
		{
			SetTask(function, time, false);
		}

		public static void SetTask(Delegate function, float time, bool repeat)
		{
			SetTask(function, time, repeat, new object[] { });
		}

		public static void SetTask(Delegate function, float time, bool repeat, object[] parameters)
		{
			Task t = new Task() {
				Function = function,
				AddTime = Server.TimeFloat,
				Time = time,
				Repeat = repeat,
				Parameters = parameters
			};

			lock (list) {
				list.Add(t);
			}
		}

		#region SetTask generic methods

		public static void SetTask<T1>(Action<T1> function, float time, bool repeat, T1 arg1)
		{
			SetTask(function, time, repeat, new object[] { arg1 });
		}

		public static void SetTask<T1, T2>(Action<T1, T2> function, float time, bool repeat, T1 arg1, T2 arg2)
		{
			SetTask(function, time, repeat, new object[] { arg1, arg2 });
		}

		public static void SetTask<T1, T2, T3>(Action<T1, T2, T3> function, float time, bool repeat, T1 arg1, T2 arg2, T3 arg3)
		{
			SetTask(function, time, repeat, new object[] { arg1, arg2, arg3 });
		}

		public static void SetTask<T1, T2, T3, T4>(Action<T1, T2, T3, T4> function, float time, bool repeat, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
		{
			SetTask(function, time, repeat, new object[] { arg1, arg2, arg3, arg4 });
		}

		public static void SetTask<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> function, float time, bool repeat, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
		{
			SetTask(function, time, repeat, new object[] { arg1, arg2, arg3, arg4, arg5 });
		}

		public static void SetTask<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> function, float time, bool repeat, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
		{
			SetTask(function, time, repeat, new object[] { arg1, arg2, arg3, arg4, arg5, arg6 });
		}

		public static void SetTask<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> function, float time, bool repeat, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
		{
			SetTask(function, time, repeat, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7 });
		}

		public static void SetTask<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> function, float time, bool repeat, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
		{
			SetTask(function, time, repeat, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8 });
		}

		public static void SetTask<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> function, float time, bool repeat, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
		{
			SetTask(function, time, repeat, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9 });
		}

		public static void SetTask<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> function, float time, bool repeat, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
		{
			SetTask(function, time, repeat, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10 });
		}

		public static void SetTask<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> function, float time, bool repeat, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
		{
			SetTask(function, time, repeat, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11 });
		}

		public static void SetTask<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> function, float time, bool repeat, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
		{
			SetTask(function, time, repeat, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12 });
		}

		public static void SetTask<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> function, float time, bool repeat, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
		{
			SetTask(function, time, repeat, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13 });
		}

		public static void SetTask<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> function, float time, bool repeat, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
		{
			SetTask(function, time, repeat, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14 });
		}

		public static void SetTask<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> function, float time, bool repeat, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
		{
			SetTask(function, time, repeat, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15 });
		}

		public static void SetTask<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> function, float time, bool repeat, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
		{
			SetTask(function, time, repeat, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16 });
		}

		#endregion

		public static void SetTask(Delegate function, TimeSpan time)
		{
			SetTask(function, time.ToFloat());
		}

		public static void SetTask(Delegate function, TimeSpan time, bool repeat)
		{
			SetTask(function, time.ToFloat(), repeat);
		}

		public static void SetTask(Delegate function, TimeSpan time, bool repeat, object[] parameters)
		{
			SetTask(function, time.ToFloat(), repeat, parameters);
		}

		internal static void WorkFrame()
		{
			lock (list) {
				List<Task> delete = new List<Task>();
				foreach (Task task in list) {
					float exectime = task.AddTime + task.Time;
					if (exectime <= Server.TimeFloat) {
						task.Function.DynamicInvoke(task.Parameters);
						if (task.Repeat) {
							task.AddTime = exectime;
						} else {
							delete.Add(task);
						}
					}
				}

				foreach (Task task in delete) {
					list.Remove(task);
				}
			}
		}
	}
}

