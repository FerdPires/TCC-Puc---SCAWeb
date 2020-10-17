import { Component, OnInit } from '@angular/core';
import { DataService } from 'src/app/data.service';

@Component({
  selector: 'app-teste',
  templateUrl: './teste.component.html',
  styleUrls: ['./teste.component.css']
})
export class TesteComponent implements OnInit {

  value: string = "";

  constructor(
    private service: DataService,
  ) { }

  ngOnInit(): void {

    this.service.teste()
      .subscribe(
        (data: any) => {
          this.value = data;

        }
      );

  }

}
