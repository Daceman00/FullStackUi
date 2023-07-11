import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Employee } from '../models/employee.model';
import { Observable } from 'rxjs/internal/Observable';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class EmployeesService {
  private url = "employees"

  baseApiUrl: string = environment.baseApiUrl;

  constructor(private http: HttpClient) { }

  getAllEmployees(): Observable<Employee[]> {
    return this.http.get<Employee[]>(`${environment.baseApiUrl}/${this.url}`);
  }

  addEmployee(addEmployeeRequest: Employee): Observable<Employee>{
    addEmployeeRequest.id = '00000000-0000-0000-0000-000000000000'
    return this.http.post<Employee>(`${environment.baseApiUrl}/${this.url}`, addEmployeeRequest);
  }

  getEmployee(id: string): Observable<Employee>{
    return this.http.get<Employee>(`${environment.baseApiUrl}/${this.url}` + '/' + id);
  }

  updateEmployee(id: string, updateEmployeeRequest: Employee): Observable<Employee>{
    return this.http.put<Employee>(`${environment.baseApiUrl}/${this.url}` + '/' + id, updateEmployeeRequest);
  }

  deleteEmployee(id: string): Observable<Employee>{
    return this.http.delete<Employee>(`${environment.baseApiUrl}/${this.url}` + '/' + id);
  }

}
