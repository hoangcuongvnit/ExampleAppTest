import { Component, ViewEncapsulation } from '@angular/core';
import { AuthService } from '../../core/auth/auth.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
    selector: 'dashboard',
    templateUrl: './dashboard.component.html',
    encapsulation: ViewEncapsulation.None,
    standalone: true,
    imports: [ CommonModule, RouterModule  ]
})
export class DashboardComponent {
    showConfirmDialog = false;

    constructor(private authService: AuthService) { }

    signOut() {
        this.showConfirmDialog = true;
    }

    confirmSignOut() {
        this.authService.signOut().subscribe(() => {
            this.showConfirmDialog = false;
            // Reload the page to ensure the user is logged out
            window.location.reload();
        });
    }

    cancelSignOut() {
        this.showConfirmDialog = false;
    }
}