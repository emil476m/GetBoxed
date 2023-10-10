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
  chartData:any[] = [] /*[
    //This is mock data and should be replaced
    {data: [65, 59, 80, 81, 56, 55, 40, 12, 87, 40, 50, 99], label: 'Box A'},
    {data: [65 ,0, 0, 3, 2, 86,1 ,79, 9, 12, 38, 64], label: 'Box B'},
    {data: [2 ,76, 34, 2, 96, 2, 3, 4, 100, 45, 28, 66], label: 'Box c'},
  ];*/
  chartLabels = ['1. January', '2. February', '3. March', '4. April', '5. May', '6. June', '7. July', '8. August', '9. September', '10. October', '11. November', '12. December'];
  chartOptions = {
    responsive: true
  };
  chartType = 'bar';

  ngOnInit(): void {
    this.getData();
  }

  async getData() {
    const result = this.http.get<boxdata>("http://localhost:5000/GraphData/");

    result.subscribe((resData) =>{
      let dataset = {
        label: resData.boxname,
        data: resData.soldPrMonth
      };
      this.chartData.push(dataset)
    })

  }


}

export interface boxdata{
  soldPrMonth : number[]
  boxname : string
  boxid : number
}


