import { FormGroup } from '@angular/forms';

// custom validator to check that two fields match
export function MustHaveUniqueChar(controlName: string) {
    return (formGroup: FormGroup) => {
        const control = formGroup.controls[controlName];

        if (control.errors) {
            return;
        }

        const doesWordContainUniqueChar = !control.value.match(/^(.)\1+$/);
        if (doesWordContainUniqueChar) {
            control.setErrors(null);
        } else {
            control.setErrors({ mustHaveUniqueChar: true });
        }
    };
}
