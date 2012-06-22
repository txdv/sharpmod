# This is another way of creating IronRuby plugins for SharpMod, it is much sorter and looks a little bit cleaner
include SharpMod

# just set the plugin information as variables
self.name        = "IronRuby short example"
self.author      = "Andrius Bentkus"
self.description = "An example which shows how to create a short plugin for IronRuby"
# you have to use System::Version for versioning your plugin
self.version     = System::Version.new 1, 0

def unload
  # this function gets called when the plugin is unloaded
end

# the load function
# there is actually no load function, you just write executing ruby-style code right away

# this will be printed right away when the plugin is loaded
# puts "Plugin Loaded"


# you have to return the self object though, since sharpmod looks if it has name, author, descript and version
# variables and other methods
return self
