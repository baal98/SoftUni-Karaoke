using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SoftUni_Karaoke
{
    class Participant
    {
        public Participant(string name, List<string> song, List<string> award)
        {
            this.Name = name;
            this.Song = song;
            this.Award = award;
        }
        public string Name { get; set; }
        public List<string> Song { get; set; }
        public List<string> Award { get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> participants = Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries).ToList();
            List<string> songsList = Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries).ToList();

            SortedDictionary<string, Participant> participantDic = new SortedDictionary<string, Participant>();

            string command;

            while ((command = Console.ReadLine()) != "dawn")
            {
                List<string> inputs = command.Split(", ", StringSplitOptions.RemoveEmptyEntries).ToList();

                if (inputs.Count != 3)
                {
                    continue;
                }
                string name = inputs[0];
                string song = inputs[1];
                string award = inputs[2];

                List<string> songs = new List<string>();
                List<string> awards = new List<string>();
                songs.Add(song);
                awards.Add(award);

                Participant participant = new Participant(name, songs, awards);

                if (!participants.Contains(name) || !songsList.Contains(song))
                {
                    continue;
                }

                if (!participantDic.ContainsKey(name))
                {
                    participantDic[name] = participant;
                }
                else
                {
                    if (participantDic[name].Song.Contains(song) && participantDic[name].Award.Contains(award))
                    {
                        continue;
                    }
                    participantDic[name].Song.Add(song);
                    participantDic[name].Award.Add(award);
                }
            }
            
            if (participantDic.Count > 0)
            {
                foreach (var participant in participantDic.OrderByDescending(x => x.Value.Award.Count).ThenBy(x => x.Key))
                {
                    Console.WriteLine($"{participant.Key}: {participant.Value.Award.Count} awards");

                    foreach (var award in participant.Value.Award.OrderBy(x => x))
                    {
                        Console.WriteLine($"--{award}");
                    }
                }
            }
            else
            {
                Console.WriteLine("No awards");
            }
        }
    }
}
