using System.Text;
using System.Text.Encodings;
using System.Text.Encodings.Web;
using System.Text.Json;

var path = "Table.csv";
//Регистрация провайдера кодировок.
//  Делается один раз в приложении.
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

// Регистрация кодировки WINDOWS - 1251 для поддержки кирилицы.
Encoding encoding = Encoding.GetEncoding(1251);


var lines = File.ReadAllLines(path);
Console.WriteLine(lines);
var workers = new Worker[lines.Length - 1];

for (int i = 1; i < lines.Length; i++)
{
    var splits = lines[i].Split(';');
    var worker = new Worker();
    worker.Фамилия = splits[0];
    worker.Производительность_в_день = Convert.ToDouble(splits[1]);
    worker.Время_работы_в_месяц = Convert.ToDouble(splits[2]);
    worker.Стоимость_вазы = Convert.ToDouble(splits[3]);
    workers[i - 1] = worker;
    //Console.WriteLine(worker);
}

var result = "result.csv";
using (StreamWriter streamWriter = new StreamWriter(result, false, encoding))
{
    streamWriter.WriteLine($"Name;Height;Weight;Coef");
    for (int i = 0; i < workers.Length; i++)
    {
        streamWriter.WriteLine(workers[i].ToExcel());
    }
}

var jsonOptions = new JsonSerializerOptions()
{
    Encoder = JavaScriptEncoder.Default
};

var json = JsonSerializer.Serialize(workers, jsonOptions);
File.WriteAllText("result.json", json);

var stringJson = File.ReadAllText("result.json");
var array = JsonSerializer.Deserialize<Worker[]>(stringJson);
foreach (var item in array)
{
    Console.WriteLine(item.ToString());
}

string jsonNewtonsoft = Newtonsoft.Json.JsonConvert.SerializeObject(workers, Newtonsoft.Json.Formatting.Indented);

//Console.WriteLine(jsonNewtonsoft);
File.WriteAllText("NewtonsoftResult.json", jsonNewtonsoft);

//var readFile = File.ReadAllText("NewtonsoftResult.json");



public class Worker
{
    public string Фамилия { get; set; }
    public double Производительность_в_день { get; set; }
    public double Время_работы_в_месяц { get; set; }

    public double Стоимость_вазы { get; set; }

    public double Количество_производимых_ваз { get => Время_работы_в_месяц * Производительность_в_день; }
    public double Месячная_зарплата_работника { get => Количество_производимых_ваз * Стоимость_вазы; }
    public override string ToString()
    {
        return $" {Фамилия}\n Производительность : {Производительность_в_день} ваз/в день\n Время работы: {Время_работы_в_месяц} дней в месяц\n Стоимость вазы: {Стоимость_вазы} рублей\n Количество производимых ваз:{Количество_производимых_ваз} в месяц\n Месячная зарплата работника:{Месячная_зарплата_работника} рублей\n";
    }

    public string ToExcel()
    {
        return $" {Фамилия};{Производительность_в_день};{Время_работы_в_месяц};{Стоимость_вазы};{Количество_производимых_ваз};{Месячная_зарплата_работника}";

    }
}