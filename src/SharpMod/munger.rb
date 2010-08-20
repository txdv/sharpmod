require "erb"
require "rexml/document"

def ReadWrite(template, output, document)
  erb = ERB.new(File.open(template).read)
  new_code = erb.result(binding)
  print "Creating #{output}\n"
  File.open("#{output}","w").write(new_code)
end

ReadWrite(ARGV[1], ARGV[2], REXML::Document.new(File.open(ARGV[0]).read))

