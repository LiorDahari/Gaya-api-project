import { Component, signal, OnInit, ChangeDetectorRef } from '@angular/core';
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
  public lastHistory: any[] = [];
  public fieldA: string = '';
  public fieldB: string = '';
  public selectedOperationId: number | null = null;
  public result: any = null;

  constructor(
    private gayaApi: GayaApi,
    private cdr: ChangeDetectorRef
  ) {}
  
  // ngOnInit(): void {
  //   this.gayaApi.ServerSideGET().subscribe(
  //     (data) => {
  //       console.log(data); //Data received from server
  //       this.operations = data; //Assign data to operations array 
  //   });
  // }  
ngOnInit(): void {
    this.gayaApi.ServerSideGET().subscribe({
      next: (ops) => {
        this.operations = ops;
        this.selectedOperationId = null;
        this.cdr.detectChanges(); // עדכון תצוגה ראשוני
      }
    });
    this.gayaApi.GetLastHistory().subscribe({
      next: (history) => {
        this.lastHistory = history;
      }
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
    this.cdr.detectChanges(); // רנדור מיד כשהתשובה מגיעה מהשרת
    this.gayaApi.GetLastHistory().subscribe({
      next: (history) => {
        this.lastHistory = history;
        this.cdr.detectChanges(); // עדכון תצוגה לאחר הצלחה בחישוב
      }
    });
  },
  error: () => {
    this.result = 'שגיאה בחישוב';
    this.cdr.detectChanges();
    }
  });
}
}