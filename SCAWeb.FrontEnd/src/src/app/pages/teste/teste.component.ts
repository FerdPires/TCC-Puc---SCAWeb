import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs/internal/Subscription';
import { finalize } from 'rxjs/operators';
import { AuthService } from 'src/app/core/services/auth.service';
import { DataService } from 'src/app/data.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-teste',
  templateUrl: './teste.component.html',
  styleUrls: ['./teste.component.css']
})
export class TesteComponent implements OnInit {
  // private readonly apiUrl = `${environment.apiUrl}authenticated`;
  // busy = false;
  // value: string = "";
  // constructor(private http: HttpClient) { }

  // ngOnInit(): void {
  //   debugger
  //   this.subscription = this.authService.user$.subscribe((x) => {
  //       const accessToken = localStorage.getItem('access_token');
  //       const refreshToken = localStorage.getItem('refresh_token');
  //       // optional touch-up: if a tab shows login page, then refresh the page to reduce duplicate login
  //     });
  //     this.service.teste()
  //       .subscribe(
  //         (data: any) => {
  //           debugger
  //           this.value = data;

  //         }
  //       );

  //   // this.busy = true;
  //   // this.http
  //   //   .get<string>(this.apiUrl)
  //   //   .pipe(finalize(() => (this.busy = false)))
  //   //   .subscribe((x) => {
  //   //     debugger
  //   //     this.value = x;
  //   //   });
  // }

  private subscription: Subscription;
  value: string = "";

  constructor(
    private service: DataService,
    private authService: AuthService,
    private route: ActivatedRoute,
    private router: Router,
  ) { }

  ngOnInit(): void {
    this.authService.user$.subscribe((x) => {
      const accessToken = localStorage.getItem('access_token');
      const refreshToken = localStorage.getItem('refresh_token');
      this.service.teste(accessToken)
        .subscribe(
          (data: any) => {
            this.value = data;

          }
        );
      // optional touch-up: if a tab shows login page, then refresh the page to reduce duplicate login
    });


  }

}
