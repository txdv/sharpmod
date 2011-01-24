<%
class MessageArgument
  attr_accessor :type, :name, :default

  def initialize(type, name, default)
    @type = type
    @name = name
    @default = default
  end

  DOT_NET_TYPES = {
    'coord' => 'int',
    'angle' => 'int',
    '*'     => 'int',
  }

  def dot_net_type
    type = DOT_NET_TYPES[@type]
    return type if not type.nil?
    return @type
  end

  def has_default
    return @default != nil
  end

  def get_write_function_name
    return "WriteLong" if @type == "*"
    return "Write" + @type.capitalize
  end
end

class Message
  attr_accessor :name, :arg_array, :message_code

  def initialize(name, arg_array, message_code)
    @name = name
    @arg_array = arg_array
    @message_code = message_code
  end

  def function_arguments
    # added default values, so don't need this one
    # return arg_array.collect { |arg| arg.dot_net_type + " " + arg.name }.join(", ")
    ret_arr = []
    arg_array.each do |arg|
      ret = "#{arg.dot_net_type} #{arg.name}"
      ret += " = #{arg.default}" if arg.has_default
      ret_arr.push ret
    end
    return ret_arr.join(", ")
  end

  def arguments
    return arg_array.collect { |arg| arg.name }.join(", ")
  end

  def has_message_code
    return @message_code != nil
  end
end

messages = []
document.root.each_element("message") do |field|
  arg_array = []
  field.each_element("argument") do |value|
    arg_array.push MessageArgument.new(value.attributes["type"], value.attributes["name"], value.attributes["default"])
  end
  code = field.elements["messagecode"]
  code = code.text if !code.nil?
  messages.push Message.new(field.attributes["name"], arg_array, code)
end 

%>using System;
using System.Reflection;

namespace SharpMod.Messages
{

  #region MessageStructs
  <% messages.each do |message| %>
  public struct <%= message.name %>Message
  {<% message.arg_array.each do |arg| %>
    public <%= arg.dot_net_type %> <%= arg.name %>;<% end %>
  }
  <% end %>

  #endregion

  #region All message functions

  public static partial class MessageFunctions
  {
    // Let's make message sending fun!
    // Let's create predefined methods for every message, so you dont have to lookup all the time everything!

    <% messages.each do |message| %>
    #region <%= message.name %>
    public static void Send<%= message.name %>Message(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity<% if message.arg_array.size > 0 then %>, <% end %>
      <%= message.function_arguments %>)
    {
      Message.Begin(destination, Message.GetUserMessageID("<%= message.name %>"), floatValue, playerEntity);
      <% if message.has_message_code then %><%= message.message_code %><% else %><% message.arg_array.each do |arg| %>
      Message.<%= arg.get_write_function_name %>(<%= arg.name %>);<% end %><% end %>

      Message.End();
    }

    public static void Send<%= message.name %>Message(MessageDestination destination, IntPtr floatValue, IntPtr playerEntity, <%= message.name %>Message val)
    {
      Send<%= message.name %>Message(destination, floatValue,playerEntity <% if message.arg_array.size > 0 then %>, <% end %><%= message.arg_array.collect { |arg| "val.#{arg.name}" }.join(", ") %>);
    }

    public static void Send<%= message.name %>Message(MessageDestination destination, IntPtr playerEntity, <%= message.name %>Message val)
    {
      Send<%= message.name %>Message(destination, IntPtr.Zero, playerEntity <% if message.arg_array.size > 0 then %>, <% end %><%= message.arg_array.collect { |arg| "val.#{arg.name}" }.join(", ") %>);
    }

    public static void Send<%= message.name %>Message(MessageDestination destination, <%= message.name %>Message val)
    {
      Send<%= message.name %>Message(destination, IntPtr.Zero, IntPtr.Zero <% if message.arg_array.size > 0 then %>, <% end %><%= message.arg_array.collect { |arg| "val.#{arg.name}" }.join(", ") %>);
    }
    
    public static void Send<%= message.name%>Message(this Player player, IntPtr floatValue, <%= message.name %>Message val)
    {
      Send<%= message.name %>Message(MessageDestination.OneReliable, floatValue, player.Pointer, val);
    }

    public static void Send<%= message.name%>Message(this Player player, <%= message.name %>Message val)
    {
      Send<%= message.name %>Message(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer, val);
    }
    
    public static void Send<%= message.name%>Message(this Player player<% if message.arg_array.size > 0 then %>, <% end %><%=message.function_arguments %>)
    {
      Send<%= message.name %>Message(MessageDestination.OneReliable, IntPtr.Zero, player.Pointer<% if message.arg_array.size > 0 then %>, <% end %><%= message.arguments %>);
    }

    #endregion
    <% end %>
  }

  #endregion


  // This is not an extensions, therefore I won't commit it it for now
  // Is it possible to create static extension methods?

  /*
  #region Player class functions
  public partial class Player
  {
    <% messages.each do |message| %>
    public static void Send<%= message.name %>Message(IntPtr floatValue, <%= message.name %>Message val)
    {
      foreach (Player player in Players) Message.Send<%= message.name%>Message(player, floatValue, val);
    }

    public static void Send<%= message.name %>Message(<%= message.name %>Message val)
    {
      foreach (Player player in Players) Message.Send<%= message.name%>Message(player, val);
    }

    public static void Send<%= message.name %>Message(<%= message.function_arguments %>)
    {
      foreach (Player player in Players) Message.Send<%= message.name%>Message(player, );
    }
    <% end %>
  }
  #endregion
  */
}
