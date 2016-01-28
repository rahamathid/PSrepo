age = 5
if (age >=5) and (age<=6)
  puts "your age is between 5 and 6"
elsif (age >=12)
  puts "your age is 12"
end

print "Enter Language :"
greeting = gets.chomp
case greeting
when "French","french"
  puts "Entered langague is French"
when "English","english"
  puts "Entered langauge is English"
else
  puts "#{greeting} No language matching"
end
