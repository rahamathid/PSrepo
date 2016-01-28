print "enter a number: "
first_num = gets.to_i
print "Enter second number: "
second_num = gets.to_i

begin
  ans = first_num / second_num
rescue
 puts "Devide failed"
 exit
 end
 puts "#{first_num} / #{second_num} = #{ans}"
