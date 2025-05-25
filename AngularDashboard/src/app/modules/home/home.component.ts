import { Component } from '@angular/core';
import { UserInformationComponent } from '../../components/user-information/user-information.component';
import { SignInComponent } from '../../components/sign-in/sign-in.component';
import { AuthService } from '../../core/auth/auth.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
  imports: [UserInformationComponent, SignInComponent, CommonModule]
})
export class HomeComponent {
  title = 'AngularDashboard';
  isAuthenticated = false;

  constructor(private _authService: AuthService) {}

  ngOnInit() {
    this._authService.check().subscribe(auth => {
      this.isAuthenticated = auth;
    });
  }
}
