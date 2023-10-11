import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";


@Component({
  selector: 'app-Graph-modal',
  template:
    `
      <ion-header>
        <ion-toolbar>
          <ion-title>Graph page for BoxOrders</ion-title>
        </ion-toolbar>
      </ion-header>

      <ion-content>
        <canvas baseChart
                [datasets]="chartData"
                [labels]="chartLabels"
                [options]="chartOptions"
        >
        </canvas>
      </ion-content>
    `,
})

export class graphModels implements OnInit {
  constructor(private http: HttpClient) {
  }
  chartData:ChartModel[] = [
    {data: [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0], label: 'Loading...'}
  ];
  chartLabels = ['1. January', '2. February', '3. March', '4. April', '5. May', '6. June', '7. July', '8. August', '9. September', '10. October', '11. November', '12. December'];
  chartOptions = {
    responsive: true
  };
  chartType = 'bar';

  ngOnInit(): void {
    this.getData();
  }

  async getData() {

    const result = this.http.get<ChartModel[]>("http://localhost:5000/GraphData/");

    result.subscribe((resData: ChartModel[]) =>{


      this.chartData = resData;
    })


  }


}

export interface boxdata{
  soldPrMonth : number[]
  boxname : string
  boxid : number
}



  export interface ChartModel {
  data: number[]
  label: string

}
