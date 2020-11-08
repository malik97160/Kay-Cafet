import { AbstractControl, ValidatorFn } from '@angular/forms';

export function PhoneNumberValidator(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } => {
        const phoneregex = RegExp("^(\\d(\\s)?){10}$");
        return phoneregex.test(control.value) ? null : { phoneNumber: 'this is not a valid phone number'};
    };
}