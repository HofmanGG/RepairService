import { Injectable, OnInit } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { NewCountry } from 'app/_models/CountryModels/NewCountry';
import { Observable, BehaviorSubject, ReplaySubject } from 'rxjs';
import { UpdateCountry } from 'app/_models/CountryModels/UpdateCountry';
import { Country } from 'app/_models/CountryModels/Country';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CountryService  {

    private countriesUrl = 'https://localhost:44305/api/countries';

    countries = new BehaviorSubject<Country[]>(null);

    constructor(private http: HttpClient) {
      this.getCountries().subscribe(
        returnedCountries => this.countries.next(returnedCountries)
      );
    }

    public addCountry(country: NewCountry) {
      return this.http.post(this.countriesUrl, country);
    }

    public updateCountry(id: number, country: UpdateCountry) {
      const url = `${this.countriesUrl}/${id}`;
      return this.http.put(url, country);
    }

    public getCountries(): Observable<Country[]> {
      return this.http.get<Country[]>(this.countriesUrl)
      .pipe(map((countries: Country[]) => {
        return countries.sort((a, b) => a.countryName.localeCompare(b.countryName));
      }));
    }

    public deleteCountry(id: number) {
      const url = `${this.countriesUrl}/${id}`;
      return this.http.delete(url);
    }
}
