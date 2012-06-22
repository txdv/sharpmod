include SharpMod
include SharpMod::CounterStrike
using_clr_extensions SharpMod::CounterStrike

self.name        = "counterstrike ruby "
self.author      = "Andrius Bentkus"
self.description = "An example which shows how to create a short plugin for IronRuby"
self.version     = System::Version.new 1, 0

Player.register_command("say /weaponmode", lambda { |player, cmd|
  weapon = player.get_active_weapon
  p weapon
  return if weapon.nil?
  p weapon.has_silencer
  weapon.silencer = !weapon.silencer if weapon.has_silencer
  weapon.burst_mode = !weapon.burst_mode if weapon.has_burst_mode
})

return self
