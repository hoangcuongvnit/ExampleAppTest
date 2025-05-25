import { Component, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../core/auth/auth.service';
import { CommonModule } from '@angular/common';

@Component({
    selector     : 'SignInForm',
    standalone   : true,
    templateUrl  : './sign-in.component.html',
    encapsulation: ViewEncapsulation.None,
    imports: [ReactiveFormsModule, CommonModule]
})
export class SignInComponent
{
    signInForm: FormGroup;
    error: string | null = null;
    showPassword = false;

    constructor(
        private _fb: FormBuilder,
        private _authService: AuthService
    ) {
        this.signInForm = this._fb.group({
            userName: ['myemail@xyz.com', [Validators.required, Validators.email]],
            password: ['Abc123!@#', Validators.required]
        });
    }

    signIn() {
        if (this.signInForm.invalid) return;
        this.error = null;
        this._authService.signIn(this.signInForm.value).subscribe({
            next: () => {
                //reload the page to ensure the user is logged in
                window.location.reload();
            },
            error: ({ error }) => {
                this.error = error?.errorMessage || 'An error occurred during sign-in.';
            }
        });
    }
}