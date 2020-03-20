import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RegisterComponent } from './_components/registration/register.component';
import { AppComponent } from './app.component';
import { LoginComponent } from './_components/login/login.component';
import { AlertComponent } from './_components/alert/alert.component';
import { AppRoutingModule } from './app-routing.module';
import { HomeComponent } from './_components/home/home.component';
import { AccountComponent } from './_components/account/account.component';
import { SelectComponent } from './shared/select/select.component';
import { UserComponent } from './_components/user/user.component';
import { UpdDelRepairRequestComponent } from './_components/user/upd-del-repair-request/upd-del-repair-request.component';
import { UserInfoComponent } from './_components/user/user-info-component/user-info.component';
import { AddRepairRequestComponent } from './_components/user/add-repair-request/add-repair-request.component';
import { SearchComponent } from './_components/search/search.component';
import { ErrorInterceptor } from './_helpers/interceptors/error.interceptor';
import { JwtInterceptor } from './_helpers/interceptors/jwt.interceptor';
import { HomeUserComponent } from './_components/home/home-user/home-user.component';
import { HomeRequestsComponent } from './_components/home/home-requests/home-requests.component';
import { ControlPanelRolesComponent } from './_components/controlpanel/control-panel-roles/control-panel-roles.component';
import { DeleteCountryComponent } from './_components/controlpanel/control-panel-countries/delete-country/delete-country.component';
import { UpdateCountryComponent } from './_components/controlpanel/control-panel-countries/update-country/update-country.component';
import { AddCountryComponent } from './_components/controlpanel/control-panel-countries/add-country/add-country.component';
import { ControlPanelCountriesComponent } from './_components/controlpanel/control-panel-countries/control-panel-countries.component';
import { AddRoleComponent } from './_components/controlpanel/control-panel-roles/add-role/add-role.component';
import { DeleteRoleComponent } from './_components/controlpanel/control-panel-roles/delete-role/delete-role.component';
import { UpdateRoleComponent } from './_components/controlpanel/control-panel-roles/update-role/update-role.component';
import { ControlPanelComponent } from './_components/controlpanel/control-panel.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule, MatFormFieldModule, MatInputModule, MatRippleModule, MatSelectModule, MatOptionModule} from '@angular/material';

@NgModule({
    imports: [
        BrowserModule,
        ReactiveFormsModule,
        HttpClientModule,
        AppRoutingModule,
        ReactiveFormsModule,
        FormsModule,
        MatButtonModule,
  MatFormFieldModule,
  MatInputModule,
  MatRippleModule,
  MatSelectModule,
  MatOptionModule,
  BrowserAnimationsModule
    ],
    declarations: [
        AppComponent,
        LoginComponent,
        RegisterComponent,
        AlertComponent,
        HomeComponent,
        AccountComponent,
        ControlPanelComponent,
        AddCountryComponent,
        DeleteCountryComponent,
        UpdateCountryComponent,
        SelectComponent,
        UserComponent,
        UpdDelRepairRequestComponent,
        UserInfoComponent,
        AddRepairRequestComponent,
        SearchComponent,
        HomeUserComponent,
        HomeRequestsComponent,
        ControlPanelCountriesComponent,
        ControlPanelRolesComponent,
        AddRoleComponent,
        DeleteRoleComponent,
        UpdateRoleComponent
    ],
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
