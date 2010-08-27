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

using System;
using System.Drawing;
using SharpMod.Messages;

namespace SharpMod.Messages
{
  public enum TextMsgPosition : int
  {
    Console1 = 1,
    Console2 = 2,
    Chat = 3,
    Center = 4,
  }

  public enum ScoreAttribute : int
  {
    /// <summary>
    /// Does nothing
    /// </summary>
    Nothing = 0,
    /// <summary>
    /// Updates the dead indication in the scoreboard
    /// </summary>
    Dead = 1,
    /// <summary>
    /// Terrorist only, updates the bomb scoreboard indication
    /// </summary>
    Bomb = 2,
    /// <summary>
    /// Counter-Terrorist only, updates the scoreboard to show the vip
    /// </summary>
    VIP = 4,
  }

  public static partial class MessageFunctions
  {

    #region Engine Chat Text Functions

    /// <summary>
    /// Prints some text in the clients chat, not colored.
    /// It sends a message in order to do so.
    /// </summary>
    /// <param name="player">
    /// Player <see cref="Player"/>
    /// </param>
    /// <param name="text">
    /// Text to print <see cref="System.String"/>
    /// </param>
    public static void ClientPrint(this Player player, string text)
    {
      player.SendTextMsgMessage((byte)3, text);
    }

    /// <summary>
    /// Prints some text in the clients chat, not colored.
    /// Use {0} for argument typing.
    /// It sends a message in order to do so.
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
    /// This function prints all the messages in an array of string to a client.
    /// It sends a message in order to do so.
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

    #region StatusIcon

    public enum StatusIconState : int
    {
      Hide = 0,
      Show,
      Flash
    };

    /// <summary>
    /// Sends the status icon message to a player.
    /// </summary>
    /// <param name="player">
    /// The player to whom to send <see cref="Player"/>
    /// </param>
    /// <param name="status">
    /// The state of the status icon (hide, show, flash) <see cref="StatusIconState"/>
    /// </param>
    /// <param name="spriteName">
    /// The sprite which to use <see cref="System.String"/>
    /// </param>
    /// <param name="color">
    /// A color <see cref="Color"/>
    /// </param>
    public static void SendStatusIconMessage(this Player player, StatusIconState status, string spriteName, Color color)
    {
      player.SendStatusIconMessage((byte)status, spriteName, color.R, color.G, color.B);
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
    public static void SendHideStatusIcon(this Player player, string spriteName)
    {
      player.SendStatusIconMessage((byte)StatusIconState.Hide, spriteName);
    }

    #endregion

    #region TeamInfo

    /// <summary>
    /// Sends a TeamInfo message to the player to inform of a teamchange
    /// The SpecialColor is set according to the Team the player is in.
    /// This is needed in order to use All 3 Counter Strike colors in chat.
    /// </summary>
    /// <param name="player">
    /// A player <see cref="Player"/>
    /// </param>
    /// <param name="team">
    /// The Team strings ("CT","TERRORIST", "SPECTATOR") <see cref="System.String"/>
    /// </param>
    public static void SendTeamInfoMessage(this Player player, string team)
    {
      player.SendTeamInfoMessage((byte)player.Index, team);
    }

    public static void SendTeamInfoMessage(this Player player, Player playerTeamChange, string team)
    {
      player.SendTeamInfoMessage((byte)playerTeamChange.Index, team);
    }

    #endregion

    #region TextMsg

    public static void SendTextMsgMessage(this Player player, TextMsgPosition position, string text)
    {
      player.SendTextMsgMessage((byte)position, text);

    }

    /// <summary>
    /// Sends a "SayText" message to a client.
    /// </summary>
    /// <param name="player">
    /// A <see cref="Player"/>
    /// </param>
    /// <param name="text">
    /// A <see cref="System.String"/>
    /// </param>
    public static void SendSayTextMessage(this Player player, string text)
    {
      // TODO: look this method up, if it is correct
      player.SendSayTextMessage((byte)1, text);
    }

    #endregion

    #region AttribMessage

    /// <summary>
    /// Informs everyone that some information changed for the player.
    /// </summary>
    /// <param name="player">
    /// The player which score attribute has changed <see cref="Player"/>
    /// </param>
    /// <param name="attrib">
    /// The score attribute <see cref="ScoreAttribute"/>
    /// </param>
    public static void SendScoreAttribMessage(this Player player, ScoreAttribute attrib)
    {
      SendScoreAttribMessage(MessageDestination.AllReliable, IntPtr.Zero, IntPtr.Zero, (byte)player.Index, (byte)attrib);
    }


    #endregion
  }
}
