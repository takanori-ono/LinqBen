using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqBen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

	// hoge
        // まずは、Where と Select
        // Where は参照のコピー
        // Select でもオブジェクトのコピーはされない 参照のコピーになる　コピーが必要なら自分でできる
        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("hoge");

            List<Hoge> hoges = new List<Hoge> {
                new Hoge {Id=1, Name="hoge"},
                new Hoge {Id=2, Name="muho"},
                new Hoge {Id=3, Name="toto"},
                new Hoge {Id=4, Name="ほげ"},
                new Hoge {Id=5, Name="hoge"},
            };

            foreach (var h in hoges)
            {
                Console.WriteLine(h.String());
            }

            var results = hoges.Where(x => x.Id == 1).Select(x => new Hoge(x));
            foreach (var h in results)
            {
                h.Name += " found";
                Console.WriteLine(h.String());
            }


            //hoges[0].Name = "henkosmasita";
            foreach (var h in hoges)
            {
                Console.WriteLine(h.String());
            }
        }

        // Range
        private void button2_Click(object sender, EventArgs e)
        {
            var nums = Enumerable.Range(1, 10);
            foreach (var n in nums)
            {
                Console.WriteLine($"{n}");
            }

            // 0,1,2,3,4,
            nums = Enumerable.Range(0, 5);
            foreach (var n in nums)
            {
                Console.WriteLine($"{n}");
            }
        }

        // 無名クラス
        // 変数スコープは？？？
        private void button3_Click(object sender, EventArgs e)
        {
            int found_val = 5;
            var list = Enumerable.Range(1, 10).Select(i =>
            {
                return new { n = i, s = i == found_val ? "bingo" : "no" }; // 無名クラス ここでfound_valが使用可
            });

            foreach (var elm in list)
            {
                Console.WriteLine($"{elm.n}:{elm.s}");
            }
        }

        // 遅延実行
        // というよりアクセスするたびに実行される
        private void button4_Click(object sender, EventArgs e)
        {
            var nums = new List<int> { 4, 5, 6, 2, };
            foreach (var elm in nums)
            {
                Console.WriteLine($"{elm}");
            }

            var orderd_nums = nums.OrderBy(n => n);

            Console.WriteLine("==================");
            nums.Add(1);
            foreach (var elm in orderd_nums)
            {
                Console.WriteLine($"{elm}");
            }

            Console.WriteLine("==================");
            nums.Add(3);
            foreach (var elm in orderd_nums)
            {
                Console.WriteLine($"{elm}");
            }

        }

        // オブジェクトの場合も button4_Click と同様アクセスのたびに実行される
        private void button5_Click(object sender, EventArgs e)
        {
            List<Hoge> hoges = new List<Hoge> {
                new Hoge {Id=5, Name="hoge"},
                //new Hoge {Id=3, Name="toto"},
                new Hoge {Id=2, Name="muho"},
                //new Hoge {Id=1, Name="hoge"},
                new Hoge {Id=4, Name="ほげ"},
            };

            foreach (var h in hoges)
            {
                Console.WriteLine(h.String());
            }

            Console.WriteLine("==================");
            var results = hoges.OrderBy(x => x.Id);
            foreach (var h in results)
            {
                Console.WriteLine(h.String());
            }

            Console.WriteLine("==================");
            hoges.Add(new Hoge { Id = 1, Name = "one", });
            foreach (var h in results)
            {
                Console.WriteLine(h.String());
            }
        }

        // Aggregate （集計）
        private void button6_Click(object sender, EventArgs e)
        {
            var nums = Enumerable.Range(1, 10).ToList();
            var all_num_str = nums.Aggregate("result: ", (result, elm) => result + $"{elm},");
            Console.WriteLine(all_num_str); // "result: 1,2,3,4,5,6,7,8,9,10,"

            nums.Add(100);
            // 以下は再度実行しないと取れない
            all_num_str = nums.Aggregate("result: ", (result, elm) => result + $"{elm},");
            Console.WriteLine(all_num_str); // "result: 1,2,3,4,5,6,7,8,9,10,100,"
        }

        // もう1つAggregate
        private void button7_Click(object sender, EventArgs e)
        {
            var list = new[]
            {
                new { Name="Hoge", Score=30 },
                new { Name="Ahe", Score=10 },
                new { Name="Bobo", Score=50 },
                new { Name="Ore", Score=100 },
            };

            foreach (var elm in list)
            {
                Console.WriteLine($"name:{elm.Name} ten:{elm.Score}");
            }

            Console.WriteLine($"max score: {list.Max(x => x.Score)}");

            // 匿名クラスを集計結果に使用しようとしたが、readonly
            //var acm = new { Idx = -1, MaxScore = 0, Count=-1 };
            //acm.Count += 1;
            //var result = list.Aggregate(acm, (res, elm) =>
            //{
            //    var aidx = ++res.Count;
            //    if ()
            //    return res;
            //});
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // Join
            var sinsei = new[] {
                new {No=123, Jiyu="ono",},
                new {No=124, Jiyu="aho",},
                new {No=125, Jiyu="baka",},
                new {No=126, Jiyu="manuke",},
            };
            var shonin = new[] {
                new {No=124, Kyoka="ok",},
                new {No=125, Kyoka="NG",},
                new {No=200, Kyoka="NG",},
            };

            var q = sinsei.Join(
                shonin,
                sno => sno.No,
                nno => nno.No,
                (selm, nelm) => new
                {
                    selm.No,
                    selm.Jiyu,
                    nelm.Kyoka,
                }
            );

            var result = q.ToList();
            //Console.WriteLine(q.ToList());
        }

        private void button9_Click(object sender, EventArgs e)
        {
            // Select のIDX
            var list = new[]
            {
                new { Name="Hoge", Score=30 },
                new { Name="Ahe", Score=10 },
                new { Name="Bobo", Score=50 },
                new { Name="Ore", Score=100 },
            };

            IEnumerable<string> result = list.Select((elm, idx) =>
            { // idx が取得可能
                return $"Name:{elm.Name} Score:{elm.Score} Idx:{idx}";
            });
            Console.WriteLine(string.Join((", "), result));

        }

        private void button10_Click(object sender, EventArgs e)
        {
            var list = new[]
            {
                new { Name="Hoge", Score=30 },
                new { Name="Ahe", Score=10 },
                new { Name="Bobo", Score=50 },
                new { Name="Ore", Score=100 },
                new { Name="Ano", Score=50 },
            };
            Console.WriteLine("single key");
            foreach (var elm in list.OrderBy(x => x.Score))
            {
                Console.WriteLine($"name:{elm.Name}, Score:{elm.Score}");
            }
            // 第2キー
            Console.WriteLine("double key");
            foreach (var elm in list.OrderBy(x => x.Score).ThenBy(x => x.Name))
            {
                Console.WriteLine($"name:{elm.Name}, Score:{elm.Score}");
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var nums = Enumerable.Range(1, 5);
            //int result = nums.Aggregate((r, elm) => r * elm); // こっちだと最初２つを最初にかける？
            int result = nums.Aggregate(1, (r, elm) => r * elm); // こっちだと最初のrが1で初期化されて最初の要素とかけられる？
            Console.WriteLine($"{result}");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            List<string> hoges = new List<string> {
                "aaa",
                " bbb",
                "BBB, ",
                "   ",
                "ccc",
            };

            var results = hoges.Select(x => x.Replace(" ", "").Replace(",", "")).Where(x => x.Length > 0);
            //var results = hoges.Where(x => x.Id == 1).Select(x => new Hoge(x));
            foreach (var h in results)
            {
                Console.WriteLine(h);
            }

        }

        // 数字列をカンマ区切り文字列に変換 sql で select * table where no in (1,2,3) などで使用
        private void button13_Click(object sender, EventArgs e)
        {
            var nums = Enumerable.Range(1, 5);
            var connect_comma = string.Join((", "), nums.Select(x => x.ToString())); // "1, 2, 3, 4, 5"
            Console.WriteLine(connect_comma);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            var list = new[]
            {
                new { Name="Hoge", Score=30 },
                new { Name="Ahe", Score=10 },
                new { Name="Bobo", Score=50 },
                new { Name="Ore", Score=100 },
            };
            var found_10 = list.Single(x => x.Score == 10);
            Console.WriteLine(found_10.ToString());

        }

        public class Hoge
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public Hoge() { }
            public Hoge(Hoge x) { Id = x.Id; Name = x.Name + " copied"; }
            public string String()
            {
                return $"{Id},{Name}";
            }

            // クラス選択してリファクタリングを選択すると、Equals の自動作成が選択可能になる
            // 以下の２つはそれで作成された物
            public override bool Equals(object obj)
            {
                var hoge = obj as Hoge;
                return hoge != null &&
                       Id == hoge.Id &&
                       Name == hoge.Name;
            }

            public override int GetHashCode()
            {
                var hashCode = -1919740922;
                hashCode = hashCode * -1521134295 + Id.GetHashCode();
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
                return hashCode;
            }
        }

        // distinct
        private void button15_Click(object sender, EventArgs e)
        {
            var smpl = new List<int> { 1,2,3,4,1,2,3,};
            var distincted = smpl.Distinct().ToList();
            Console.WriteLine(string.Join((", "),distincted));
        }

        private void button16_Click_1(object sender, EventArgs e)
        {
            string aaa = "abc\n\rde";
            aaa.Where(x => {
                Console.WriteLine(x);
                return true;
            }).Count();

            string new_aaa = new string(aaa.Where(x => !char.IsControl(x)).ToArray());

            new_aaa.Where(x => {
                Console.WriteLine(x);
                return true;
            }).Count();

        }

        // 昇順にして各数字がどの位置（index）か
        private void button17_Click(object sender, EventArgs e)
        {
            var smpl = new List<int> { 5, 3, 10, 22, 3, 4, 10, 22, 3, };
            var henkan = smpl.Distinct().OrderBy(x => x).ToList();
            Console.WriteLine(string.Join((", "), henkan)); // 3,4,5,10,22

            Console.WriteLine(henkan.FindIndex(x => x == 10)); // 3
            Console.WriteLine(henkan.FindIndex(x => x == 3)); // 0
            Console.WriteLine(henkan.FindIndex(x => x == 22)); // 4
        }

        public class Hoge2
        {
            public int No { get; set; }
            public string Name { get; set; }
        }
        // distinct をクラスでやると？
        private void button18_Click(object sender, EventArgs e)
        {
            var smpl = new List<Hoge2>
            {
                new Hoge2 {No = 1, Name = "ono" },
                new Hoge2 {No = 2, Name = "desu" },
                new Hoge2 {No = 1, Name = "hoho" },
                new Hoge2 {No = 2, Name = "toto" },
                new Hoge2 {No = 1, Name = "ono" },
            };
            var disticted = smpl.Distinct().ToList();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            // first() で取得すると、参照にならない？？？ 勘違い。そんなことはない。
            var smpl = new List<Hoge2>
            {
                new Hoge2 {No = 1, Name = "ono" },
                new Hoge2 {No = 2, Name = "desu" },
                new Hoge2 {No = 3, Name = "hoho" },
                new Hoge2 {No = 4, Name = "toto" },
            };

            smpl.Where(x => x.No == 3).First().Name = "onodesuyo";

            foreach (var h in smpl)
            {
                Console.WriteLine($"No: {h.No}, Name: {h.Name}");
            }

        }

        public class PetOwners
        {
            public string Name { get; set; }
            public List<string> Pets { get; set; }
        }
        // SelectMany 2段階のリストを平滑化する
        private void button20_Click(object sender, EventArgs e)
        {
            PetOwners[] petOwners =
            {
                new PetOwners {Name = "aaa", Pets = new List<string>{"neco","inu",} },
                new PetOwners {Name = "ono", Pets = new List<string>{"kiji","saru",} },
            };

            foreach (var a in petOwners)
            {
                Console.WriteLine($"{a.Name} has pets {string.Join(":", a.Pets)}");
            }

            // 1番目は、要素をもらって、内部リストを返すようにする
            // 2番目は、要素と内部リストの１要素を貰えるので、result に合うように加工する
            var q = petOwners.SelectMany(x => x.Pets, (p, c) => new { Name = p.Name, Pet = c });
            foreach(var b in q)
            {
                Console.WriteLine($"{b.Name} has pets {b.Pet}");
            }
        }
    }
}
