import { Injectable } from '@angular/core';
import { StatusCodes } from 'app/_models/StatusCodes';

@Injectable({
  providedIn: 'root'
})
export class StatusCodeService {

  constructor() { }

  public changeStatusCode(statusCodes: StatusCodes, statusCode: number) {
     if (statusCode === 400) {
      statusCodes.badRequest = true;
    } else if (statusCode === 401) {
      statusCodes.unauthorized = true;
    } else if (statusCode === 403) {
      statusCodes.forbidden = true;
    } else if (statusCode === 404) {
      statusCodes.notFound = true;
    } else if (statusCode === 409) {
      statusCodes.conflict = true;
    } else {
      statusCodes.unknownError = true;
    }
  }

  public resetStatusCodes(statusCodes: StatusCodes) {
    statusCodes.success = false;
    statusCodes.badRequest = false;
    statusCodes.unauthorized = false;
    statusCodes.forbidden = false;
    statusCodes.notFound = false;
    statusCodes.conflict = false;
    statusCodes.unknownError = false;
  }
}
