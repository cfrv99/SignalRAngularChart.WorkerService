import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ChartModel } from './chart.model';
import { SignalRService } from './services/signal-r.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{

  title = 'chart-signalR';
  public data: ChartModel[];
  public chartOptions: any = {
    scaleShowVerticalLines: true,
    responsive: true,
    scales: {
      yAxes: [{
        ticks: {
          beginAtZero: true
        }
      }]
    }
  };
  public chartLabels: string[] = ['Real time data for the chart'];
  public chartType: string = 'bar';
  public chartLegend: boolean = true;
  public colors: any[] =
  [{ backgroundColor: '#5491DA' },
  { backgroundColor: '#E74C3C' },
  { backgroundColor: '#82E0AA' },
  { backgroundColor: '#E5E7E9' }];

  constructor(public signalRService: SignalRService,private http: HttpClient){
  }
  ngOnInit(): void {
    this.signalRService.startConnection();
    this.signalRService.addTransferChartDataListener();
    this.startHttpRequest();
    this.data = this.signalRService.data;
    this.signalRService.listenBroadCast();
  }

  private startHttpRequest = () => {
    this.http.get('https://localhost:5001/api/chart')
      .subscribe(res => {
        console.log(res);
      });
  }

  public Test=()=>{
    console.log("Data: "+this.data);
  }
}
