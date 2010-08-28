include SharpMod
include SharpMod::CounterStrike
using_clr_extensions SharpMod::CounterStrike

self.name        = "Bank"
self.author      = "Andrius Bentkus"
self.description = "A bank plugin implemented with IronRuby"
self.version     = System::Version.new 0, 1

$bank_accounts = {}

class SharpMod::Player
  def bank_id
    return self.AuthID
  end

  def bank_open
    $bank_accounts[bank_id] = 0
  end

  def bank_close
    $bank_accounts.delete bank_id

  end

  def has_bank_account
    $bank_accounts.has_key?(bank_id)
  end

  def bank_deposit(amount)
    return false if (amount > get_money)
    p $bank_accounts
    $bank_accounts[bank_id] += amount
    return true
  end

  def bank_withdraw(amount)
    return false if (amount > bank_balance)
    $bank_accounts[bank_id] += amount
    return true
  end

  def bank_balance
    return $bank_accounts[bank_id]
  end
end

# TODO bank_default_opening = CVar.new("bank_default_opening", "0")
bank_state           = CVar.new("bank_state", "1")
# TODO bank_min_players     = CVar.new("bank_min_players", "2")
# TODO bank_restrict        = CVar.new("bank_restrict", "0")
# TODO bank_fees_base       = CVar.new("bank_fees_base", "0")
# TODO bank_fees_increase   = CVar.new("bank_fees_base", "0")
# TODO bank_offrounds       = CVar.new("bank_offrounds", "2")
# TODO bank_msg_interval
# TODO bank_msg
# TODO bank_interest_rate   = CVar.new("bank_interest_rate", "0.01")

class CVar
  def num
    return string.to_i
  end
end

Player.register_command "bank_open", lambda { |player, cmd|
  if !player.has_bank_account then
    player.bank_open
    player.print_console "Bank account opened, your balance is 0"
  else
    player.print_console "You already have a bank account"
  end
}


Player.register_command "bank_close", lambda { |player, cmd|
  if player.bank_close
    player.print_console "Your bank accoutn has been closed, all your money belongs to the server"
  else
    player.print_console "You have no bank account"
  end
}


Player.register_command "bank_deposit", lambda { |player, cmd|
  return if cmd.arguments.size < 2
  if !player.has_bank_account then
    player.print_console "You don't have a bank account"
    return
  end
  #if Player.players.size < bank_min_players.num then
  #  player.print_console "Minimum #{bank_min_players.num} have to be on the server to use the bank"
  #  return
  #end

  amount = cmd.arguments[1].to_i
  if player.bank_deposit(amount) then
    player.set_money(player.get_money - amount, true)
    player.print_console "Your balance: #{player.bank_balance}"
  else
    player.print_console "You don't have so much money!"
  end
}

Player.register_command "bank_withdraw", lambda { |player, cmd|
  return if cmd.arguments.size < 2
  if !player.has_bank_account then
    player.print_console "You don't have a bank account"
    return
  end
  #if Player.players.size < bank_min_players.string.to_i then
  #  player.print_console "Minimum #{bank_min_players.string.to_i} have to be on the server to use the bank"
  #  return
  #end

  if player.bank_withdraw(mount) then
    player.print_console "Your balance: #{player.bank_balance}"
    player.set_money(player.get_money + amount, true)
  else
    player.print_console "You don't have so much money on your account"
  end
}
