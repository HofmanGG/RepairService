import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthenticationService } from '@core/_services/authentication.service';


@Injectable({ providedIn: 'root' })
export class ManagerGuard implements CanActivate {
    constructor(
        private router: Router,
        private authenticationService: AuthenticationService
    ) {}

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        const doesManagerRoleExist = this.authenticationService.currentUserValue.roles.includes('Manager');
        if (doesManagerRoleExist) {
            return true;
        }

        this.router.navigate(['/home'], { queryParams: { returnUrl: state.url }});
        return false;
    }
}