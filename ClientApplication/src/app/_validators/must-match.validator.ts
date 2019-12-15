import { FormGroup } from '@angular/forms';

// custom validator to check that two fields match
export function MustMatch(controlName: string, matchingControlName: string) {
    return (formGroup: FormGroup) => {
        const control = formGroup.controls[controlName];
        const matchingControl = formGroup.controls[matchingControlName];

        if (matchingControl.errors && !matchingControl.errors.mustMatch) {
            // return if another validator has already found an error on the matchingControl
            return;
        }

        const doPasswordsMatch = control.value === matchingControl.value;
        if (doPasswordsMatch) {
            matchingControl.setErrors(null);
        } else {
            matchingControl.setErrors({ mustMatch: true });
        }
    };
}
