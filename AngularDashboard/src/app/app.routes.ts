import { Routes } from '@angular/router';
import { AuthGuard } from './core/auth/guards/auth.guard';
import { NoAuthGuard } from './core/auth/guards/noAuth.guard';
import { DashboardComponent } from './modules/dashboard/dashboard.component';
import { HomeComponent } from './modules/home/home.component';
import { AuthSignOutComponent } from './modules/sign-out/sign-out.component';
import { EmptyLayoutComponent } from './modules/layouts/empty.component';

export const routes: Routes = [

    // hoemepage route
    {   path: '',
        component: HomeComponent,
        pathMatch: 'full'
    },

    // guestspage route
    {
        path: '',
        canActivate: [NoAuthGuard],
        canActivateChild: [NoAuthGuard],
        component: EmptyLayoutComponent,
        children: [
            {path: 'guests', loadChildren: () => import('./modules/guests/guests.routes')},
        ]
    },

    // admin routes
    {
        path: '',
        canActivate: [AuthGuard],
        canActivateChild: [AuthGuard],
        component: EmptyLayoutComponent,
        children: [
            // dashboard
            {path: 'dashboard', component: DashboardComponent},
            {path: 'sign-out', component: AuthSignOutComponent},
        ]
    }
];
