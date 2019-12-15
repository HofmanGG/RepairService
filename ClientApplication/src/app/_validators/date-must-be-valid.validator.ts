import { FormGroup } from '@angular/forms';
import * as moment from 'moment';

// custom validator to check that two fields match
export function DateMustBeValid(dayKey: string, monthKey: string) {
    return (formGroup: FormGroup) => {
        const dayControl = formGroup.controls[dayKey];
        const monthControl = formGroup.controls[monthKey];

        const areThereErrorsAlready = dayControl.errors || monthControl.errors;
        if (areThereErrorsAlready && !dayControl.errors.dateMustBeValid) {
            return;
        }

        const day = +dayControl.value;
        const month = +monthControl.value;

        const monthWhenMaxDayIsTwentyEight = [2];
        const monthWhenMaxDayIsThirty = [4, 6, 9, 11];
        const monthWhenMaxDayIsThirtyOne = [1, 3, 5, 7, 8, 10, 12];

        const dateIsValid = (
        (day <= 28 && monthWhenMaxDayIsTwentyEight.includes(month)) ||
        (day <= 30 && monthWhenMaxDayIsThirty.includes(month)) ||
        (day <= 31 && monthWhenMaxDayIsThirtyOne.includes(month)));

        if (dateIsValid) {
            formGroup.controls[dayKey].setErrors(null);
        } else {
            formGroup.controls[dayKey].setErrors({ dateMustBeValid: true });
        }
    };
}
