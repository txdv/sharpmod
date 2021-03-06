/*
hlsdk/multiplayer/common/const.h:

#define MSG_BROADCAST   0   // unreliable to all
#define MSG_ONE       1   // reliable to one (msg_entity)
#define MSG_ALL       2   // reliable to all
#define MSG_INIT      3   // write to the init string
#define MSG_PVS       4   // Ents in PVS of org
#define MSG_PAS       5   // Ents in PAS of org
#define MSG_PVS_R     6   // Reliable to PVS
#define MSG_PAS_R     7   // Reliable to PAS
#define MSG_ONE_UNRELIABLE  8   // Send to one client, but don't put in reliable stream, put in unreliable datagram ( could be dropped )
#define MSG_SPEC      9   // Sends to all spectator proxies
*/

using System;
using System.Collections.Generic;
using SharpMod.MetaMod;
using SharpMod.Helper;
using SharpMod.Messages;
using SharpMod.Math;

namespace SharpMod
{

	/// <summary>
	/// An enum for MessageDestinations, used by Message.Begin
	/// </summary>
	public enum MessageDestination
	{
		BroadCast = 0,
		OneReliable,
		AllReliable,
		Init,
		PVS,
		PAS,
		PVSR,
		PASR,
		OneUnreliable,
		Spectator
	}

	public struct MessageHeader
	{
		public MessageHeader(MessageDestination destination, int messageType, IntPtr floatValue, IntPtr entity)
		{
			this.Destination = destination;
			this.MessageType = messageType;
			this.FloatValue = floatValue;
			this.Entity = entity;
		}

		public MessageDestination Destination;
		public int MessageType;
		public IntPtr FloatValue;
		public IntPtr Entity;

		public Player Player {
			get {
				return Entity == IntPtr.Zero ? null : Player.GetPlayer(Entity);
			}
		}
	}

	public class MessageArgument
	{
		public Type Type { get; protected set; }
		public string ShortTypeName { get { return Type.ToString().Split('.').Last().ToLower(); } }
		public object Value { get; protected set; }
		public DateTime CallTime { get; protected set; }

		public MessageArgument(Type type, object value)
		{
			CallTime = DateTime.Now;
			Type = type;
			Value = value;
		}

		public override string ToString ()
		{
			return string.Format("{0}: {1} @ {2}", Type, Value, CallTime);
		}
	}

	public class MessageInformation
	{
		public MessageDestination MessageDestination { get; protected set; }
		public int MessageType { get; protected set; }
		public IntPtr Value { get; protected set; }
		public IntPtr PlayerEdict { get; protected set; }
		public List<MessageArgument> Arguments { get; protected set; }

		public DateTime CallTimeBegin { get; set; }
		public DateTime CallTimeEnd { get; set; }

		public MessageInformation(MessageDestination destination, int messageType, IntPtr val, IntPtr playerEdict)
		{
			MessageDestination = destination;
			MessageType = messageType;
			Value = val;
			PlayerEdict = playerEdict;
			Arguments = new List<MessageArgument>();
		}

		public string MessageName { get { return Message.GetUserMessageName(MessageType); } }

		/// <summary>
		/// Returns information about the message in one compact string
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/>
		/// </returns>
		public string OneLineInfo()
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			sb.Append("{0}({1})", MessageName, MessageType);

			sb.Append("({0}, {1}, {2})", MessageDestination, Value, PlayerEdict);

			sb.Append("(");
			for (int i = 0; i < Arguments.Count; i++) {
				MessageArgument argument = Arguments[i];
				if (argument.Type == typeof(string)) {
					sb.Append("{0}:{1}", argument.ShortTypeName, (argument.Value as string).Escape());
				} else {
					sb.Append("{0}:{1}", argument.ShortTypeName, argument.Value);
				}
				if (i + 1 < Arguments.Count) {
					sb.Append(", ");
				}
			}
			sb.Append(");");
			return sb.ToString();
		}

		/// <summary>
		/// returns information in a lot of strings
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/>
		/// </returns>
		public string VerboseInfo()
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			sb.Append("\nMessage: {0}\n", MessageName);
			sb.Append("\n\tMessageDestination={0}", MessageDestination);
			sb.Append("\n\tMessageType={0}", MessageType);
			sb.Append("\n\tValue={0}", Value);
			sb.Append("\n\tPlayerEdict={0}", PlayerEdict);
			sb.Append("\n\tCallTimeBegin={0}", CallTimeBegin);
			sb.Append("\n\tCallTimeEnd={0}", CallTimeEnd);
			foreach (var arg in Arguments) {
				sb.Append("\n\t\t{0}", arg);
			}
			return sb.ToString();
		}

		public override string ToString()
		{
			// TODO: Create a way to switch between the 2 representation modes
			return OneLineInfo();
		}

	}

	/// <summary>
	/// A wrapper class for sending messages with the goldsrc engine
	/// </summary>
	public static class Message
	{
		internal static void Init()
		{
			for (int i = 0; i < 64; i++) {
				TypeNames[i] = new BinaryTree.Node("", i);
			}
		}

		#if DEBUG
		private static MessageInformation messageInformation;
		#endif

		/// <summary>
		/// Calls the Engine to retrieve the User Message ID
		/// </summary>
		/// <param name="name">
		/// The name of the message <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// The ID (type) of the user message.<see cref="System.Int32"/>
		/// </returns>
		public static int GetUserMessageID(string name)
		{
			return MetaModEngine.metaUtilityFunctions.GetUserMsgID(MetaModEngine.PLID, name, 0);
		}

		public static string GetUserMessageName(int id)
		{
			return (id >= 64 ? Message.TypeNames[id].Name : "");
		}

		/// <summary>
		/// A private count for the arguments send by the function.
		/// </summary>
		private static int count = 0;

		private static BinaryTree Types = new BinaryTree();
		private static BinaryTree.Node[] TypeNames = new BinaryTree.Node[500];

		/// <summary>
		/// The maximum length of a message, set by the goldsrc engine.
		/// </summary>
		public const int MaxLength = 192;

		#region Engine Message Functions

		#region Base Message Functions
		/// <summary>
		/// Begins with the sending of a message.
		/// </summary>
		/// <param name="destination">
		/// The destination of a message. <see cref="MessageDestination"/>
		/// </param>
		/// <param name="msg_type">
		/// The integer value of the message type. <see cref="System.Int32"/>
		/// </param>
		/// <param name="flValue">
		/// A float value? <see cref="IntPtr"/>
		/// </param>
		/// <param name="player">
		/// If send to a specific player, the player entity has to be supplied here <see cref="IntPtr"/>
		/// </param>
		public static void Begin(MessageDestination destination, int messageType, IntPtr floatValue, IntPtr playerEntity)
		{
			#if DEBUG
			messageInformation = new MessageInformation(destination, messageType, floatValue, playerEntity);
			messageInformation.CallTimeBegin = DateTime.Now;
			#endif

			MetaModEngine.engineFunctions.MessageBegin(destination, messageType, floatValue, playerEntity);
			count = 0;
		}

		/// <summary>
		/// Signals the engine that the message is *constructed* and that it can be send.
		/// </summary>
		public static void End()
		{
			#if DEBUG
			messageInformation.CallTimeEnd = DateTime.Now;
			Console.WriteLine ("Custom: {0}", messageInformation);
			#endif

			MetaModEngine.engineFunctions.MessageEnd();
		}

		#endregion

		// some information in:
		// parsemsg.cpp

		#region WriteByte

		/// <summary>
		/// Writes a byte value.
		/// If the message buffer is already full, writing will be omitted.
		/// </summary>
		/// <param name="val">
		/// A byte value. <see cref="System.Byte"/>
		/// </param>
		public static void WriteByte(byte val)
		{
			#if DEBUG
			messageInformation.Arguments.Add(new MessageArgument(typeof(byte), (byte)val));
			#endif
			if (count + sizeof(byte) < MaxLength) {
				MetaModEngine.engineFunctions.WriteByte((int)val);
				count += sizeof(byte);
			}
		}

		/// <summary>
		/// Writes a byte value.
		/// If the message buffer is already full, writing will be omitted.
		/// </summary>
		/// <param name="val">
		/// A byte value. <see cref="System.Byte"/>
		/// </param>
		public static void Write(byte val)
		{
			WriteByte(val);
		}

		#endregion
		#region WriteChar

		/// <summary>
		/// Writes a character into the message
		/// If the message buffer is already full, writing will be omitted.
		/// </summary>
		/// <param name="val">
		/// A character value <see cref="System.Char"/>
		/// </param>
		public static void WriteChar(char val)
		{
			#if DEBUG
			messageInformation.Arguments.Add(new MessageArgument(typeof(char), (char)val));
			#endif
			if (count + sizeof(char) < MaxLength) {
				MetaModEngine.engineFunctions.WriteChar((int)val);
				count += sizeof(char);
			}
		}

		/// <summary>
		/// Writes a character into the message
		/// If the message buffer is already full, writing will be omitted.
		/// </summary>
		/// <param name="val">
		/// A character value <see cref="System.Char"/>
		/// </param>
		public static void Write(char val)
		{
			WriteChar(val);
		}

		#endregion
		#region WriteShort

		/// <summary>
		/// Writes a short into the message.
		/// If the message buffer is already full, writing will be omitted.
		/// </summary>
		/// <param name="val">
		/// A character value <see cref="System.Char"/>
		/// </param>
		public static void WriteShort(short val)
		{
			#if DEBUG
			messageInformation.Arguments.Add(new MessageArgument(typeof(short), (short)val));
			#endif
			if (count + sizeof(short) < MaxLength) {
				MetaModEngine.engineFunctions.WriteShort(val);
				count += sizeof(short);
			}
		}

		/// <summary>
		/// Writes a short into the message.
		/// If the message buffer is already full, writing will be omitted.
		/// </summary>
		/// <param name="val">
		/// A character value <see cref="System.Char"/>
		/// </param>
		public static void Write(short val)
		{
			WriteShort(val);
		}

		#endregion
		#region WriteLong

		/// <summary>
		/// Writes a long in the message.
		/// </summary>
		/// <param name="val">
		/// A long value <see cref="System.Int64"/>
		/// If the message buffer is already full, writing will be omitted.
		/// </param>
		public static void WriteLong(long val)
		{
			#if DEBUG
			messageInformation.Arguments.Add(new MessageArgument(typeof(long), (long)val));
			#endif
			if (count+sizeof(long) < MaxLength) {
				MetaModEngine.engineFunctions.WriteLong((int)val);
				count += sizeof(long);
			}
		}

		public static void WriteLong(int val)
		{
			WriteLong((long)val);
		}

		/// <summary>
		/// Writes a long in the message.
		/// </summary>
		/// <param name="val">
		/// A long value <see cref="System.Int64"/>
		/// If the message buffer is already full, writing will be omitted.
		/// </param>
		public static void Write(long val)
		{
			WriteLong(val);
		}

		#endregion
		#region WriteAngle

		// TODO: Check if WriteAngle is implemented correctly

		/// <summary>
		/// Writes an angle in the message.
		/// </summary>
		/// <param name="val">
		/// An angle value <see cref="System.Int32"/>
		/// If the message buffer is already full, writing will be omitted.
		/// </param>
		public static void WriteAngle(int val)
		{
			#if DEBUG
			messageInformation.Arguments.Add(new MessageArgument(typeof(int), val));
			#endif

			// TODO: check if this really what the WriteAngle functions sends

			if (count + sizeof(int) < MaxLength) {
				MetaModEngine.engineFunctions.WriteAngle(val);
				count += sizeof(int);
			}
		}

		#endregion
		#region WriteCoord

		// TODO: Check if WriteCoord is implemented correctly

		public static void WriteCoord(Vector3f val)
		{
			#if DEBUG
			messageInformation.Arguments.Add(new MessageArgument(typeof(Vector3f), val));
			#endif
			if (count + sizeof(float) * 3 < MaxLength) {
				WriteCoord(val.x);
				WriteCoord(val.x);
				WriteCoord(val.x);
			}
		}

		unsafe internal static void WriteCoord(float val)
		{
			int *intValue = (int *)&val;
			MetaModEngine.engineFunctions.WriteAngle(*intValue);
		}

		/*
		/// <summary>
		/// Writes a coord value into the message.
		/// </summary>
		/// <param name="val">
		/// A coord value <see cref="System.Int32"/>
		/// If the message buffer is already full, writing will be omitted.
		/// </param>
		public static void WriteCoord(int val)
		{
			#if DEBUG
			messageInformation.Arguments.Add(new MessageArgument(typeof(int), val));
			#endif

			// TODO: check if this really what the WriteEntity functions sends

			if (count + sizeof(int) < MaxLength) {
				MetaModEngine.engineFunctions.WriteCoord(val);
				count += sizeof(int);
			}
		}
		*/

		#endregion
		#region WriteString

		/// <summary>
		/// Writes a string value into the buffer.
		/// If the string is too long and would result in a message overflow, it will be truncated.
		/// </summary>
		/// <param name="val">
		/// A <see cref="System.String"/>
		/// </param>
		public static void WriteString(string val)
		{
			if (val == null) {
				return;
			}
			#if DEBUG
			messageInformation.Arguments.Add(new MessageArgument(typeof(string), (string)val));
			#endif
			if (count + val.Length >= MaxLength) {
				MetaModEngine.engineFunctions.WriteString(val.Substring(0, count+val.Length-MaxLength-1));
			} else {
				MetaModEngine.engineFunctions.WriteString(val);
			}
		}

		/// <summary>
		/// Writes a string value into the buffer.
		/// If the string is too long and would result in a message overflow, it will be truncated.
		/// </summary>
		/// <param name="val">
		/// A <see cref="System.String"/>
		/// </param>
		public static void Write(string val)
		{
			WriteString(val);
		}

		#endregion
		#region WriteEntity

		public static void WriteEntity(int entity)
		{
			#if DEBUG
			messageInformation.Arguments.Add(new MessageArgument(typeof(Entity), entity));
			#endif

			// TODO: check if this really what the WriteEntity functions sends

			if (count + sizeof(int) < MaxLength) {
				MetaModEngine.engineFunctions.WriteEntity((int)entity);
				count += sizeof(int);
			}
		}

		public static void WriteEntity(Entity entity)
		{
			// TODO: check if this really what the WriteEntity functions sends

			WriteEntity(entity.Index);
		}

		public static void Write(Entity entity)
		{
			WriteEntity(entity);
		}

		#endregion

		#endregion

		public static int Register(string name, int size)
		{
			int val = Message.Types.Count + 64;
			BinaryTree.Node node = new BinaryTree.Node(name, val);
			Message.Types.Add(node);
			Message.TypeNames[val] = node;
			return val;
		}

		public static bool Intercept(string name, Delegate del)
		{
			var node = Message.Types.GetNode(name);

			if (node == null) {
				return false;
			}

			node.invokerlist.Add(del);
			return true;
		}

		public static bool Intercept(int id, Delegate del)
		{
			var node = Message.TypeNames[id];

			if (node == null) {
				return false;
			}

			node.invokerlist.Add(del);
			return true;
		}

		internal static void Invoke(Delegate del, MessageHeader message_header, List<object> parameters)
		{
			var param = del.Method.GetParameters();

			object[] argumentList = new object[param.Length];

			int last = 0;
			for (int i = 0; i < param.Length; i++) {
				Type t = param[i].ParameterType;

				// handle special arguments first
				if (t == typeof(MessageHeader)) {
					argumentList[i] = message_header;
					last++;
				} else if (t == typeof(Player)) {
					argumentList[i] = message_header.Player;
					last++;
				} else if (t.IsMessage()) {
					int j = 0;
					object o = Activator.CreateInstance(t);
					foreach (var pi in t.GetFields()) {
						if (j < parameters.Count) {
							pi.SetValue(o, parameters[j]);
						}
						j++;
					}
					argumentList[i] = o;
					last++;
				} else {
					break;
				}
			}

			// copy the parameters in order for the rest
			parameters.CopyTo(0, argumentList, last, param.Length - last);
			for (int i = last + parameters.Count; i < param.Length; i++) {
				argumentList[i] = param[i].DefaultValue;
			}

			del.Method.Invoke(null, argumentList);
		}

		internal static void Invoke(List<Delegate> list, MessageHeader message_header, List<object> parameters)
		{
			foreach (Delegate del in list) {
				Invoke(del, message_header, parameters);
			}
		}

		internal static void Invoke(MessageHeader message_header, List<object> parameters)
		{
			BinaryTree.Node node = Message.TypeNames[message_header.MessageType];

			if (node == null) {
				return;
			}

			Invoke(node.invokerlist, message_header, parameters);
		}

		enum PluginFunctions
		{
			Continue,
			Handled,
		}

		/// <summary>
		/// HudElements enum for use in HideWeapons function
		/// </summary>
		public enum HudElements : byte
		{
			CrosshairAmmoWeapons = 1 << 0,
			Flashlight           = 1 << 1,
			All                  = 1 << 2,
			RadarHealthArmor     = 1 << 3,
			Timer                = 1 << 4,
			Money                = 1 << 5,
			Crosshair            = 1 << 6,
			Nothing              = 1 << 7
		}

	}

	/// <summary>
	/// The BinaryTree class to efficiently look up the integer identifiers
	/// associated them to the text identifiers
	/// </summary>
	public class BinaryTree
	{
		public class Node
		{
			private string name;
			private int val;
			internal Node left;
			internal Node right;
			public List<Delegate> invokerlist = new List<Delegate>();

			public string Name { get { return name; } }
			public int Value { get { return val; } }

			public Node(string name, int val)
			{
				this.name = name;
				this.val = val;
			}
		}

		protected BinaryTree.Node root = null;
		protected int count = 0;

		public int Count {
			get { return count; }
		}

		public BinaryTree()
		{
		}

		public void Add(string name, int val)
		{
			Add(new BinaryTree.Node(name, val));
		}

		public void Add(Node node)
		{
			Add(ref root, node);
		}
		private void Add(ref BinaryTree.Node position, BinaryTree.Node newNode)
		{
			if (position == null) {
				position = newNode;
				count++;
				return;
			}
			int comparison = String.Compare(position.Name, newNode.Name);
			if (comparison == 0) {
				#if DEBUG
				Console.WriteLine ("{0} {1}", position.Name, newNode.Name);
				#endif
			} else if (comparison < 0) {
				Add(ref position.left, newNode);
			} else if (comparison > 0) {
				Add(ref position.right, newNode);
			}
		}

		public Node GetNode(string name)
		{
			return GetNode(root, name);
		}

		public Node GetNode(Node position, string name)
		{
			if (position == null) {
				return null;
			}
			int comparison = String.Compare(position.Name, name);
			if (comparison == 0) {
				return position;
			} else if (comparison < 0) {
				return GetNode(position.left, name);
			} else {
				return GetNode(position.right, name);
			}
		}

		public int GetValue(string name)
		{
			Node node = GetNode(name);
			if (node == null) {
				return -1;
			} else {
				return node.Value;
			}
		}
	}
}
