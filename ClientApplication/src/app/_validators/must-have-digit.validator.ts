import { FormGroup } from '@angular/forms';

// custom validator to check that two fields match
export function MustHaveDigit(controlName: string) {
    return (formGroup: FormGroup) => {
        const control = formGroup.controls[controlName];

        if (control.errors) {
            return;
        }

        const doesWordContainDigit = control.value.match(/\d/);
        if (doesWordContainDigit) {
            control.setErrors(null);
        } else {
            control.setErrors({ mustHaveDigit: true });
        }
    };
}
