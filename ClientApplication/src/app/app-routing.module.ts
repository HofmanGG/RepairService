import { Routes, RouterModule } from '@angular/router';

import { RegisterComponent } from './_components/registration/register.component';
import { LoginComponent } from './_components/login/login.component';
import { HomeComponent } from './_components/home/home.component';
import { AccountComponent } from './_components/account/account.component';
import { UserComponent } from './_components/user/user.component';
import { SearchComponent } from './_components/search/search.component';

import { ManagerGuard } from './_helpers/guards/manager.guard';
import { AuthGuard } from './_helpers/guards/auth.guard';
import { AdminGuard } from './_helpers/guards/admin.guard';
import { AddRepairRequestComponent } from './_components/user/add-repair-request/add-repair-request.component';
import { UpdDelRepairRequestComponent } from './_components/user/upd-del-repair-request/upd-del-repair-request.component';
import { ControlPanelRolesComponent } from './_components/controlpanel/control-panel-roles/control-panel-roles.component';
import { ControlPanelCountriesComponent } from './_components/controlpanel/control-panel-countries/control-panel-countries.component';
import { ControlPanelComponent } from './_components/controlpanel/control-panel.component';

const userRoutes: Routes = [
    { path: 'addrequest', component: AddRepairRequestComponent},
    { path: 'upddelrequest', component: UpdDelRepairRequestComponent}
];

const controlPanelRoutes: Routes = [
    { path: 'countries', component: ControlPanelCountriesComponent},
    { path: 'roles', component: ControlPanelRolesComponent}
];

const routes: Routes = [
    { path: '', component: HomeComponent, canActivate: [AuthGuard] },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'account', component: AccountComponent, canActivate: [AuthGuard] },
    { path: 'users/:id', component: UserComponent, children: userRoutes, canActivate: [ManagerGuard] },
    { path: 'controlpanel', component: ControlPanelComponent, children: controlPanelRoutes, canActivate: [AdminGuard] },
    { path: 'search', component: SearchComponent, canActivate: [ManagerGuard] },

    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];

export const AppRoutingModule = RouterModule.forRoot(routes);
