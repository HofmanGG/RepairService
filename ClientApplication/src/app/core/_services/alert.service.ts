import { Injectable } from '@angular/core';
import { Router, NavigationStart } from '@angular/router';
import { Observable, Subject } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AlertService {
    private subject = new Subject<any>();
    private keepAfterRouteChange = false;

    constructor(private router: Router) {
        // clear alert messages on route change unless 'keepAfterRouteChange' flag is true
        this.router.events.subscribe(event => {
            if (event instanceof NavigationStart) {
                if (this.keepAfterRouteChange) {
                    // only keep for a single route change
                    this.keepAfterRouteChange = false;
                } else {
                    // clear alert message
                    this.clear();
                }
            }
        });
    }

    public getAlert(): Observable<any> {
      return this.subject.asObservable();
    }

    public success(message: string, keepAfterRouteChange = false): void {
      this.keepAfterRouteChange = keepAfterRouteChange;
      this.subject.next({ type: 'success', text: message });
    }

    public error(message: string, keepAfterRouteChange = false): void {
        this.keepAfterRouteChange = keepAfterRouteChange;
        this.subject.next({ type: 'error', text: message });
    }

    public clear(): void {
        // clear by calling subject.next() without parameters
        this.subject.next();
    }
}
