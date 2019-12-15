import { FormGroup } from '@angular/forms';

// custom validator to check that two fields match
export function MustHaveNonAlphanumeric(controlName: string) {
    return (formGroup: FormGroup) => {
        const control = formGroup.controls[controlName];

        if (control.errors) {
            return;
        }

        const doesWordContainNonAlphanumeric = control.value.match(/\W/);
        if (doesWordContainNonAlphanumeric) {
            control.setErrors(null);
        } else {
            control.setErrors({ mustHaveNonAlphanumeric: true });
        }
    };
}
