import { Injectable } from "@angular/core";
import * as signalR from "@aspnet/signalr";
import { environment } from "src/environments/environment";
import { ChartModel } from "../chart.model";

@Injectable({
  providedIn: "root",
})
export class SignalRService {
  public data: ChartModel[];
  private hubConnection: signalR.HubConnection;
  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(environment.hubUrl)
      .build();
    this.hubConnection
      .start()
      .then(() => console.log("Connection started"))
      .catch((err) => console.log("Error while starting connection: " + err));
  };
  public addTransferChartDataListener = () => {
    this.hubConnection.on('transferchartdata', (data) => {
      this.data = data;
    });
  };

  public listenBroadCast = () => {
    this.hubConnection.on('ReceiveData', (message) => {
      console.log(message);
    });
  };
}
