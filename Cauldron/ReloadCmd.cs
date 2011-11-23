using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TShockAPI;

namespace Cauldron
{
    class ReloadCmd:Cmd
    {
        public override void exec()
        {
            if (ply.Group.HasPermission("cauldron-reload"))
            {
                Console.WriteLine("Potions are being reloaded from file.");
                Cauldron.potions.Clear();

                String file = Path.Combine(TShock.SavePath, "potions.txt");
                if (File.Exists(file))
                {
                    using (var sr = new StreamReader(file))
                    {
                        String line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            String[] info = line.Split();
                            if (info.Length >= 4)
                            {
                                String pName = info[0];
                                int l = 0;
                                int.TryParse(info[1], out l);
                                String per = info[2];
                                Potion p = new Potion(pName, l, per);
                                for (int i = 3; i < info.Length; i++)
                                {
                                    int id = 0;
                                    int.TryParse(info[i], out id);
                                    p.addPotion(id);
                                }
                                Cauldron.potions.Add(pName.ToLower(), p);
                                Console.WriteLine(String.Format("Potion {0} added.", pName));
                            }
                        }
                    }
                }
            }
            else
            {
                ply.SendMessage( "You do not have access to this command.", Color.Red);
            }
        }
    }
}
