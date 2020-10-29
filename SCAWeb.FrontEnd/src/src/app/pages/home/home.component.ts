import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  accessToken = '';
  refreshToken = '';
  user_role = '';

  constructor(
    public authService: AuthService
  ) { }

  ngOnInit(): void {
    this.authService.user$.subscribe(x => {
      if (x) {
        this.user_role = x.role;
      }
    });
    this.accessToken = localStorage.getItem('access_token');
    this.refreshToken = localStorage.getItem('refresh_token');
  }

  logout() {
    this.authService.logout();
  }
}
