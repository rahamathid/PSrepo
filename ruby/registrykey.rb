require 'win32/registry'
keyname= 'SOFTWARE'

# KEY_ALL_ACCESS enables you to write and deleted.
# the default access is KEY_READ if you specify nothing
access = Win32::Registry::KEY_ALL_ACCESS

Win32::Registry::HKEY_LOCAL_MACHINE.open(keyname, access) do |reg|
  # each is the same as each_value, because #each_key actually means
  # "each child folder" so #each doesn't list any child folders...
  # use #keys for that...
  reg.each{|name, value| print name, value}
end
