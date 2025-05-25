import { Component, ViewEncapsulation } from '@angular/core';
import { AuthService } from '../../core/auth/auth.service';
import { Router } from '@angular/router';

@Component({
    selector     : 'UserInformation',
    standalone   : true,
    templateUrl  : './user-information.component.html',
    encapsulation: ViewEncapsulation.None,
})
export class UserInformationComponent
{
    name = 'John Doe';
    age = 30;
    gender = 'Male';
    username = 'johndoe';
    description = 'A passionate developer who loves coding and learning new technologies.';
    /**
     * Constructor
     */
    constructor(private authService: AuthService, private router: Router) {}

    onSignOut(): void {
        this.authService.signOut().subscribe(() => {
            this.router.navigate(['/']);
            //Reload the page to ensure the user is logged out
            window.location.reload();
        });
    }
}
