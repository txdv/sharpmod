include SharpMod

=begin
  This is one way of writing Ruby plugins,
  Create a class, include the IPlugin interface, override the constructor
  and set the information as shown in the initialize method
=end

class FieldInfoPlugin
  include IPlugin
  # initialize the fields, make them publicly available
  attr_reader :name, :author, :description, :version

  def initialize
    # set the variables
    @name        = "IronRuby Plugin example"
    @author      = "Andrius Bentkus"
    @description = "An example which shows how to create a plugin for IronRuby"
    # you have to use System::Version for versioning your plugin
    @version     = System::Version.new 1, 0
  end

  def load
    # this method is called when the plugin is loaded
    # you can do event hooking here
  end

  def unload
    # this method is called when the plugin is unloaded
  end

end

# this has to be here, since the DLR doesn't support instance creating of non
# existent ruby objects, actually it does, but not in a way I want
# SharpMod will take this instance and load it into 
FieldInfoPlugin.new
