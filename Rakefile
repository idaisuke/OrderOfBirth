task :setup do

  dll_list.each { |dll|
    old_path = dll
    new_path = File.expand_path("Dependencies/#{File.basename(dll)}", File.dirname(__FILE__))

    if File.exists?(new_path)
      File.delete(new_path)
    end

    puts "Create symlink Dependencies/#{File.basename(dll)}"
    File.symlink(old_path, new_path)
  }

end

def dll_list
  if (/cygwin|mswin|mingw|bccwin|wince|emx/ =~ RUBY_PLATFORM) != nil
    # Windows
    require 'win32/registry'

    reg = Win32::Registry::HKEY_LOCAL_MACHINE.open('SOFTWARE\WOW6432Node\Valve\Steam', Win32::Registry::KEY_READ)

    steam_path = File.join(reg['InstallPath'], 'steamapps')

    [
        File.join(steam_path, "common/RimWorld/RimWorldWin64_Data/Managed/Assembly-CSharp.dll"),
        File.join(steam_path, "common/RimWorld/RimWorldWin64_Data/Managed/UnityEngine.dll"),
        File.join(steam_path, "common/RimWorld/RimWorldWin64_Data/Managed/UnityEngine.CoreModule.dll"),
        File.join(steam_path, "workshop/content/294100/2009463077/Current/Assemblies/0Harmony.dll"),
    ]

  elsif (/darwin/ =~ RUBY_PLATFORM) != nil
    # macOS
    steam_path = File.expand_path("~/Library/Application Support/Steam/steamapps")

    [
        File.join(steam_path, "common/RimWorld/RimWorldMac.app/Contents/Resources/Data/Managed/Assembly-CSharp.dll"),
        File.join(steam_path, "common/RimWorld/RimWorldMac.app/Contents/Resources/Data/Managed/UnityEngine.dll"),
        File.join(steam_path, "common/RimWorld/RimWorldMac.app/Contents/Resources/Data/Managed/UnityEngine.CoreModule.dll"),
        File.join(steam_path, "workshop/content/294100/2009463077/Current/Assemblies/0Harmony.dll"),
    ]
  end
end