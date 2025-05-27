import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Approval } from './approval.types';
import { map, Observable, ReplaySubject, tap } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class ApprovalService {
    private _httpClient = inject(HttpClient);
    private _approvals: ReplaySubject<Approval[]> = new ReplaySubject<Approval[]>(1);
    private readonly baseUrl = 'http://localhost:7213/api';

    getApprovals(): Observable<Approval[]> {
        return this._httpClient.get<{ ErrorMessage: string; IsSuccess: boolean; Value: Approval[] }>(`${this.baseUrl}/approvals`).pipe(
            map(response => response.Value),
            tap(approvals => this._approvals.next(approvals))
        );
    }

    createApproval(): Observable<any> {
        return this._httpClient.post(`${this.baseUrl}/approval`, {});
    }

    approve(instanceId: string): Observable<any> {
        return this._httpClient.post(`${this.baseUrl}/approve/${instanceId}`, {});
    }

    reject(instanceId: string): Observable<any> {
        return this._httpClient.post(`${this.baseUrl}/reject/${instanceId}`, {});
    }

    startApproval(requestId: string): Observable<any> {
        return this._httpClient.post(`${this.baseUrl}/start-approval/${requestId}`, {});
    }

    cleanApprovals(): Observable<any> {
        return this._httpClient.post(`${this.baseUrl}/approval/clean`, {});
    }
}
