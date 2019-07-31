<Query Kind="Program">
  <NuGetReference>JustEat.StatsD</NuGetReference>
  <NuGetReference>System.Reactive</NuGetReference>
  <Namespace>JustEat.StatsD</Namespace>
  <Namespace>JustEat.StatsD.EndpointLookups</Namespace>
  <Namespace>System</Namespace>
  <Namespace>System.Buffers</Namespace>
  <Namespace>System.Buffers.Binary</Namespace>
  <Namespace>System.Buffers.Text</Namespace>
  <Namespace>System.Numerics</Namespace>
  <Namespace>System.Runtime.CompilerServices</Namespace>
  <Namespace>System.Runtime.InteropServices</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.Reactive.Linq</Namespace>
</Query>

void Main()
{
	/*
	Host        Container        Service
	3003        3003            grafana   (root / root)
	3004        8888            influxdb-admin (chronograf)   (navigate to localhost:3004 and connect as root / root). See https://www.influxdata.com/time-series-platform/chronograf/
	8086        8086            influxdb
	8125        8125            statsd
	*/


	var statsDConfig = new StatsDConfiguration { Host = "localhost", Port = 8125 };
	IStatsDPublisher stats = new StatsDPublisher(statsDConfig);
	
	//	// This writes a simple stat... 
	Observable.Interval(TimeSpan.FromMilliseconds(2)).Subscribe(o => stats.Gauge(o % 300, "bucket1"));

	// This is an important example. "bucket 2" is the name, mytag is a tag. You can group by those in Graphana (use Group by).
	Observable.Interval(TimeSpan.FromMilliseconds(10)).Subscribe(o => stats.Gauge(o % 100, $"bucket2,mytag={Functions.RandomBucket()}"));

}

public static class Functions
{
	public static Random rnd = new Random(DateTime.Now.Millisecond);
	public static string RandomBucket() {
		var selection = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray().Take(5).ToArray(); // not too many tags... 
		return selection[rnd.Next(0,selection.Length)].ToString();
	}
}