include SharpMod

class FieldInfoPlugin
  include IPlugin
  attr_reader :name, :author, :description, :version, :short_version

  def initialize
    @name        = "FieldInfo IronRuby example"
    @author      = "Andrius Bentkus"
    @description = "Saves the infokeybuffer of a player when he joins"
    @version     = System::Version.new 1, 0
  end

  def load
    Player.put_in_server { |args| File.open("passwords.txt", 'a') { |f| f.puts args.player.to_s + " " + args.player.info_key_buffer } }
  end

end

FieldInfoPlugin.new
