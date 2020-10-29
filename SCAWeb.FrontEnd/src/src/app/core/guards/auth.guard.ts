import { Injectable } from '@angular/core';
import {
    CanActivate,
    ActivatedRouteSnapshot,
    RouterStateSnapshot,
    UrlTree,
    Router,
} from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { map } from 'rxjs/operators';

@Injectable({
    providedIn: 'root',
})
export class AuthGuard implements CanActivate {
    constructor(private router: Router, private authService: AuthService) { }

    canActivate(
        next: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ):
        | Observable<boolean | UrlTree>
        | Promise<boolean | UrlTree>
        | boolean
        | UrlTree {
        return this.authService.user$.pipe(
            map((user) => {
                if (user) {
                    if (state.url == "/area-risco" && user.role != "admin") {
                        this.router.navigate(['login'], {
                            queryParams: { returnUrl: "/" },
                        });
                        return false;
                    } else if (state.url == "/sensores-risco" && user.role != "admin") {
                        this.router.navigate(['login'], {
                            queryParams: { returnUrl: "/" },
                        });
                        return false;
                    } else if (state.url == "/alertas" && user.role != "admin") {
                        this.router.navigate(['login'], {
                            queryParams: { returnUrl: "/" },
                        });
                        return false;
                    }
                    return true;
                } else {
                    this.router.navigate(['login'], {
                        queryParams: { returnUrl: state.url },
                    });
                    return false;
                }
            })
        );
    }
}
