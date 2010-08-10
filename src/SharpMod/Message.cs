//
//     This file is part of sharpmod.
//     sharpmod is a metamod plugin which enables you to write plugins
//     for Valve GoldSrc using .NET programms.
// 
//     Copyright (C) 2010  Andrius Bentkus
// 
//     csharpmod is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     csharpmod is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with csharpmod.  If not, see <http://www.gnu.org/licenses/>.
//

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
using System.Drawing;

namespace SharpMod
{

  /// <summary>
  /// A struct for all the HudMessage values
  /// </summary>
  public struct HudMessageStruct
  {
    public float x,y;
    public int effect;
    public byte r1, g1, b1, a1;
    public byte r2, g2, b2, a2;
    public float fadeinTime;
    public float fadeoutTime;
    public float holdTime;
    public float fxTime;
    public int channel;
  }

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

  /// <summary>
  /// A wrapper class for sending messages with the goldsrc engine
  /// </summary>
  public static class Message
  {
    static Message()
    {
      Tree<MessageHandler> t = new Tree<MessageHandler>();

    }
    /// <summary>
    /// A private count for the arguments send by the function.
    /// </summary>
    private static int count = 0;

    public static BinaryTree Types = new BinaryTree();
    public static BinaryTree.Node[] TypeNames = new BinaryTree.Node[500];

    /// <summary>
    /// The maximum length of a message, set by the goldsrc engine.
    /// </summary>
    public const int MaxLength = 192;

    #region Engine Message Functions

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
    public static void Begin(MessageDestination destination, int msg_type, IntPtr flValue, IntPtr playerEntity)
    {
      MetaModEngine.engineFunctions.MessageBegin(destination, msg_type, flValue, playerEntity);
      count = 0;
    }

    /// <summary>
    /// Signals the engine that the message is *constructed* and that it can be send.
    /// </summary>
    public static void End()
    {
      MetaModEngine.engineFunctions.MessageEnd();
      //EngineInterface.MessageEnd();
    }

    /// <summary>
    /// Writes a character into the message
    /// If the message buffer is already full, writing will be omitted.
    /// </summary>
    /// <param name="val">
    /// A character value <see cref="System.Char"/>
    /// </param>
    public static void WriteChar(char val)
    {
      if (count+sizeof(char) < MaxLength)
      {
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

    /// <summary>
    /// Writes a long in the message.
    /// </summary>
    /// <param name="val">
    /// A long value <see cref="System.Int64"/>
    /// If the message buffer is already full, writing will be omitted.
    /// </param>
    public static void WriteLong(long val)
    {
      if (count+sizeof(long) < MaxLength)
      {
        MetaModEngine.engineFunctions.WriteLong((int)val);
        count += sizeof(long);
      }
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


    /// <summary>
    /// Writes a byte value.
    /// If the message buffer is already full, writing will be omitted.
    /// </summary>
    /// <param name="val">
    /// A byte value. <see cref="System.Byte"/>
    /// </param>
    public static void WriteByte(byte val)
    {
      if (count+sizeof(byte) < MaxLength)
      {
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


    /// <summary>
    /// Writes a string value into the buffer.
    /// If the string is too long and would result in a message overflow, it will be truncated.
    /// </summary>
    /// <param name="val">
    /// A <see cref="System.String"/>
    /// </param>
    public static void WriteString(string val)
    {
      if (count+val.Length >= MaxLength)
      {
        MetaModEngine.engineFunctions.WriteString(val.Substring(0, count+val.Length-MaxLength-1));
      }
      else
      {
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

    /// <summary>
    /// Writes a short into the message.
    /// If the message buffer is already full, writing will be omitted.
    /// </summary>
    /// <param name="val">
    /// A character value <see cref="System.Char"/>
    /// </param>
    public static void WriteShort(short val)
    {
      if (count+sizeof(short) < MaxLength)
      {
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


    #region Engine Chat Text Functions

    /// <summary>
    /// Prints some text in the clients chat, not colored.
    /// Use {0} for argument typing.
    /// </summary>
    /// <param name="player">
    /// Player <see cref="Player"/>
    /// </param>
    /// <param name="text">
    /// Text <see cref="System.String"/>
    /// </param>
    /// <param name="obj">
    /// Arguments <see cref="System.Object[]"/>
    /// </param>
    public static void ClientPrint(this Player player, string text, object[] obj)
    {
      ClientPrint(player, String.Format(text, obj));
    }

    /// <summary>
    /// Prints some text in the clients chat, not colored.
    /// </summary>
    /// <param name="player">
    /// Player <see cref="Player"/>
    /// </param>
    /// <param name="text">
    /// Text to print <see cref="System.String"/>
    /// </param>
    public static void ClientPrint(this Player player, string text)
    {
      Message.Begin(MessageDestination.OneReliable, Message.Types.GetValue("TextMsg"), IntPtr.Zero, player.Pointer);
      Message.Write((byte)3); // printchat
      Message.Write(text);
      Message.End();
    }

    /// <summary>
    /// This function prints all the messages in an array of string to a client.
    /// </summary>
    /// <param name="player">
    /// Player <see cref="Player"/>
    /// </param>
    /// <param name="text">
    /// Array of string <see cref="System.String[]"/>
    /// </param>
    public static void ClientPrint(this Player player, string[] text)
    {
      foreach (string line in text)
      {
        player.ClientPrint(line);
      }
    }
    /// <summary>
    /// Splits the string in an array (determines boundaries by \n and \r) and prints each string
    /// </summary>
    /// <param name="player">
    /// Player <see cref="Player"/>
    /// </param>
    /// <param name="text">
    /// String with \r and \n for line determination. <see cref="System.String"/>
    /// </param>
    public static void ClientPrintEachLine(this Player player, string text)
    {
      player.ClientPrint(text.Split(new char[] {'\n', '\r'}));
    }

    #endregion

    /// <summary>
    /// A function for internal use, HudMessage uses this in order to scale some values.
    /// </summary>
    /// <param name="val">
    /// The original value. <see cref="System.Single"/>
    /// </param>
    /// <param name="scale">
    /// The scale factor. <see cref="System.Single"/>
    /// </param>
    /// <returns>
    /// The result in 16bit int form. <see cref="System.Int16"/>
    /// </returns>
    private static short FixedUnsigned16(float val, float scale)
    {
      int output;
      output = (int)((float)val * scale);
      if (output >  32767) output =  32767;
      if (output < -32768) output = -32768;
      return (short)output;
    }
    /// <summary>
    /// Sends a hudmessage to a player.
    /// The player will see a hudmessage on his screen.
    /// </summary>
    /// <param name="player">
    /// The player. <see cref="Player"/>
    /// </param>
    /// <param name="textparams">
    /// A struct with all the hudmessages. <see cref="HudMessageStruct"/>
    /// </param>
    /// <param name="message">
    /// The text to draw on the hud. <see cref="System.String"/>
    /// </param>
    public static void HudMessage(this Player player, HudMessageStruct textparams, string message)
    {
      if (player == null)
        Message.Begin(MessageDestination.BroadCast, 23, IntPtr.Zero, IntPtr.Zero);
      else
        Message.Begin(MessageDestination.OneReliable, 23, IntPtr.Zero, player.Pointer);

      Message.Write((byte)29);
      Message.Write((byte)(textparams.channel & 0xFF));
      Message.Write(FixedUnsigned16(textparams.x, 1 << 13));
      Message.Write(FixedUnsigned16(textparams.y, 1 << 13));
      Message.Write((byte)textparams.effect);

      Message.Write(textparams.r1);
      Message.Write(textparams.g1);
      Message.Write(textparams.b1);
      Message.Write(textparams.a1);

      Message.Write(textparams.r2);
      Message.Write(textparams.g2);
      Message.Write(textparams.b2);
      Message.Write(textparams.a2);

      Message.Write(FixedUnsigned16(textparams.fadeinTime, 1<<8));
      Message.Write(FixedUnsigned16(textparams.fadeoutTime, 1<<8));
      Message.Write(FixedUnsigned16(textparams.holdTime, 1<<8));

      if (textparams.effect == 2)
        Message.Write(FixedUnsigned16(textparams.fxTime, 1<<8));

      Message.Write(message);
      Message.End();
    }

    /// <summary>
    /// Sends a client the HideWeapon message in order to hide hud elements.
    /// </summary>
    /// <param name="player">
    /// The player which to use. <see cref="Player"/>
    /// </param>
    /// <param name="elements">
    /// The elements which to hide, use the enum HudElements <see cref="System.Byte"/>
    /// </param>
    public static void HideWeapons(this Player player, byte elements)
    {
      Message.Begin(MessageDestination.OneReliable, Message.Types.GetValue("HideWeapon"), IntPtr.Zero, player.Pointer);
      Message.Write(elements);
      Message.End();
    }

    #region StatusIcon

    public enum StatusIconState : byte
    {
      Hide = 0,
      Show,
      Flash
    };

    public static void StatusIcon(this Player player, StatusIconState status, string spriteName, Color color)
    {
      StatusIcon(player, status, spriteName, color.R, color.G, color.B);
    }

    public static void StatusIcon(this Player player, StatusIconState status, string spriteName, byte r, byte g, byte b)
    {
      Message.Begin(MessageDestination.OneReliable, Message.Types.GetValue("StatusIcon"), IntPtr.Zero, player.Pointer);
      Message.WriteByte((byte)status);
      Message.Write(spriteName);
      if (status != StatusIconState.Hide)
      {
        Message.Write(r);
        Message.Write(g);
        Message.Write(b);
      }
      Message.End();
    }

    /// <summary>
    /// Sends a message to show a status icon
    /// </summary>
    /// <param name="player">
    /// A player <see cref="Player"/>
    /// </param>
    /// <param name="spriteName">
    /// The spritname of the status icon <see cref="System.String"/>
    /// </param>
    public static void HideStatusIcon(this Player player, string spriteName)
    {
      StatusIcon(player, StatusIconState.Hide, spriteName, 0, 0, 0);
    }

    public static void VGUIMenu(this Player player, byte menuID, short keysBitSum, char time, byte multipart, string name)
    {
      Message.Begin(MessageDestination.OneReliable, Message.Types.GetValue("VGUIMenu"), IntPtr.Zero, player.Pointer);
      Message.End();
    }

    public static void VoiceMask(this Player player, long AudiblePlayerIndexBitSum, long ServerBannedPlayersIndexbitSum)
    {
      Message.Begin(MessageDestination.OneReliable, Message.Types.GetValue("VoiceMask"), IntPtr.Zero, player.Pointer);
      Message.Write(AudiblePlayerIndexBitSum);
      Message.Write(ServerBannedPlayersIndexbitSum);
      Message.End();
    }

    public static void SendWeaponListMessage(this Player player, string WeaponName, byte PrimaryAmmoID, byte PrimaryAmmoMaxAmount,
                                             byte SecondaryAmmoID, byte SecondaryAmmoMaxAmount, byte SlotID, byte NumberInSlot, byte WeaponID,
                                             byte Flags)
    {
      Message.Begin(MessageDestination.OneReliable, Message.Types.GetValue("WeaponList"), IntPtr.Zero, player.Pointer);
      Message.Write(WeaponName);
      Message.Write(PrimaryAmmoID);
      Message.Write(PrimaryAmmoMaxAmount);
      Message.Write(SecondaryAmmoID);
      Message.Write(SecondaryAmmoMaxAmount);
      Message.Write(SlotID);
      Message.Write(NumberInSlot);
      Message.Write(WeaponID);
      Message.Write(Flags);
      Message.End();
    }

    public static void SendWeaponPickupMessage(this Player player, byte weapon)
    {
      Message.Begin(MessageDestination.OneReliable, Message.Types.GetValue("WeapPickup"), IntPtr.Zero, player.Pointer);
      Message.Write(weapon);
      Message.End();
    }

    #endregion

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

  public class MessageHandler : IComparable<MessageHandler>
  {
    public string Name { get; protected set; }
    public int Value { get; protected set; }

    private List<Delegate> handlers;
    public MessageHandler(string name, int val)
    {
      Name = name;
      Value = val;
      handlers = new List<Delegate>();
    }

    public int AddHandler(Delegate handler)
    {
      handlers.Add(handler);
      return handlers.Count-1;
    }

    public bool RemoveHandler(Delegate handler)
    {
      int count = handlers.Count;
      handlers.Remove(handler);
      return (count == handlers.Count);
    }
    public bool RemoveHandler(int index)
    {
      if (index < handlers.Count)
      {
        handlers.RemoveAt(index);
        return true;
      }
      else return false;
    }

    public void Invoke(object[] list)
    {
      foreach (Delegate handler in handlers)
      {
        if (Invoke(handler, list) == PluginFunctions.Handled)
        {
          return;
        }
      }
    }
    private PluginFunctions Invoke(Delegate handler, object[] list)
    {
      var parameters = handler.Method.GetParameters();
      int count = list.Length - parameters.Length;
      List<object> newlist = new List<object>(list);
      if (count > 0) {
        // remove some
        newlist.RemoveRange(parameters.Length, count);
      }
      else {
        // add some
        for (int i = list.Length; i < parameters.Length; i++)
        {
          newlist.Add(parameters[i].DefaultValue);
        }
      }

      object ret = handler.Method.Invoke(null, newlist.ToArray());
      if (ret.GetType() is PluginFunctions) return (PluginFunctions)ret;
      else return PluginFunctions.Continue;
    }

    #region IComparable<T> implementation
    public int CompareTo (MessageHandler other)
    {
      return this.Name.CompareTo(other.Name);
    }
    #endregion
  }

  public class Tree<T> where T : IComparable<T> {
    public class Node<T> {
      public Node<T> left, right;
      public T Value { get; protected set; }
      public Node(T val)
      {
        Value = val;
      }
    }

    protected Node<T> root = null;
    protected int count = 0;

    public Tree()
    {
    }

    public int Count {
      get {
        return count;
      }
    }

    public void Add(T val)
    {
      Add(new Node<T>(val));
    }

    public void Add(Node<T> newNode)
    {
      Add(ref root, newNode);
    }

    private void Add(ref Node<T> current, Node<T> newNode)
    {
      if (current == null) { current = newNode; count++; return; }


      int comparison = current.Value.CompareTo(newNode.Value);
      if (comparison == 0)
      {
        throw new ArgumentException("This node already exists");
      }
      else if (comparison < 0) Add(ref current.left,  newNode);
      else                     Add(ref current.right, newNode);
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
      public Delegate invoker;

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

    public int Count
    {
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
      if (position == null) { position = newNode; count++; return; }
      int comparison = String.Compare(position.Name, newNode.Name);
      if (comparison == 0)
      {
        Console.WriteLine ("{0} {1}", position.Name, newNode.Name);
        Console.WriteLine ("NESAMONE");
      }
      else if (comparison < 0) Add(ref position.left, newNode);
      else if (comparison > 0) Add(ref position.right, newNode);
    }

    public Node GetNode(string name)
    {
      return GetNode(root, name);
    }

    public Node GetNode(Node position, string name)
    {
      if (position == null) return null;
      int comparison = String.Compare(position.Name, name);
           if (comparison == 0) return position;
      else if (comparison < 0)  return GetNode(position.left, name);
      else                      return GetNode(position.right, name);
    }

    public int GetValue(string name)
    {
      Node node = GetNode(name);
      if (node == null) return -1;
      else return node.Value;
    }
  }
}