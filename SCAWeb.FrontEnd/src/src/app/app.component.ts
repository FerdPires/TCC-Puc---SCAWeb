import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './core';

@Component({
  selector: 'app-root',
  template: '<router-outlet></router-outlet>'
})
export class AppComponent implements OnInit {
  constructor(
    private authService: AuthService,
    private router: Router
  ) {
  }
  ngOnInit(): void { }
  // ngOnInit(): void {
  //   this.authService.onAuthStateChanged(data => {
  //     if (data) {
  //       this.router.navigateByUrl('/');
  //     } else {
  //       this.router.navigateByUrl('/login');
  //     }
  //   });
  // }
}
