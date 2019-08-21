    
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { Country } from 'src/app/_models/Country';
import { CountryService } from 'src/app/_services/country.service';
import { UserService } from 'src/app/_services/user.service';
import { AlertService } from 'src/app/_services/alert.service';


@Component({ templateUrl: 'register.component.html' })
export class RegisterComponent implements OnInit {
    registerForm: FormGroup;
    loading = false;
    submitted = false;
    countries: Country[]

    constructor(
        private formBuilder: FormBuilder,
        private router: Router,
        private authenticationService: AuthenticationService,
        private userService: UserService,
        private alertService: AlertService,
        private countriesService: CountryService
    ) {
        // redirect to home if already logged in
        if (this.authenticationService.currentUserValue) {
            this.router.navigate(['/']);
        }
    }

    ngOnInit() {
        this.registerForm = this.formBuilder.group({
            firstName: ['', Validators.required],
            lastName: ['', Validators.required],
            email: ['', Validators.required],
            password: ['', [Validators.required, Validators.minLength(6)]],
            confirmPassword: ['', Validators.required],
            gender: ['', Validators.required],
            dayOfBirth: ['', Validators.required], 
            monthOfBirth: ['', Validators.required], 
            yearOfBirth: ['', Validators.required], 
            countryId: ['', Validators.required]
        });
        this.countriesService.getCountries()
        .subscribe((countryModels: Country[]) => this.countries = countryModels)
    }

    // convenience getter for easy access to form fields
    get f() { return this.registerForm.controls; }

    onSubmit() {
        this.submitted = true;

        // reset alerts on submit
        this.alertService.clear();

        // stop here if form is invalid
        if (this.registerForm.invalid) {
            return;
        }
        let value = this.registerForm.value;
        this.loading = true;
        this.userService.register(
            value.email, 
            value.password, 
            value.confirmPassword, 
            value.firstName, 
            value.lastName, 
            value.gender,
            value.dayOfBirth,
            value.monthOfBirth,
            value.yearOfBirth,
            value.countryId)
            .pipe(first())
            .subscribe(
                data => {
                    this.alertService.success('Registration successful', true);
                    this.router.navigate(['/login']);
                },
                error => {
                    this.alertService.error(error);
                    this.loading = false;
                });
    }
}