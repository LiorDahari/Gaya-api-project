import { Component, signal, OnInit  } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { GayaApi } from './services/gaya-api'; 
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  imports: [RouterOutlet, CommonModule, FormsModule],
  selector: 'app-root',
  styleUrl: './app.css',
  templateUrl: './app.html',
})
export class App implements OnInit {
  protected readonly title = signal('gaya-client');
  public operations: any[] = [];

  public fieldA: string = '';
  public fieldB: string = '';
  public selectedOperationId: number | null = null;
  public result: any = null;

  constructor(private gayaApi: GayaApi) {}
  
  // ngOnInit(): void {
  //   this.gayaApi.ServerSideGET().subscribe(
  //     (data) => {
  //       console.log(data); //Data received from server
  //       this.operations = data; //Assign data to operations array 
  //   });
  // }  
  ngOnInit() {
    this.gayaApi.ServerSideGET().subscribe(ops => {
    this.operations = ops;
    this.selectedOperationId = null;
    });
  }
  calculate(): void {
     if (this.selectedOperationId === null) {
      this.result = 'יש לבחור פעולה';
      return;
    }
    const request = {
      valueA: this.fieldA,
      valueB: this.fieldB,
      operationId: this.selectedOperationId
  };

    this.gayaApi.Calculate(request).subscribe({
  next: (response) => {
    this.result = response.result;
  },
  error: () => {
    this.result = 'שגיאה בחישוב';
    }
  });
}
}