class animal
  def initialize
    puts "Creating a new animal"
  end

  def set_name(new_name)
    @name = new_name
  end

  def get_name
    @name
  end

  def name
    @name
  end

  def name=(new_name)
    if new_name.is_a?(Numberic)
      puts "Name cannot be a number"
    else
      @name = new_name
    end
  end
end


cat = animal.new
cat.set_name("catname")
puts cat.get_name
