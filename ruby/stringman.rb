mulline = <<EOM
this is a multi
line comments
EOM
puts mulline

first_name = "rahamath"
last_name = "ulla"
full_name ="#{first_name} #{last_name}"
puts full_name
puts "Full Name contains the value : #{full_name.include?("ra")}"
puts full_name.start_with?("ulla")
puts first_name.equal?"rahamath"
