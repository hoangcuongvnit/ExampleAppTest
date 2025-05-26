import { Component, ViewEncapsulation } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

interface Approval {
    requestedBy: string;
    requestedEmail: string;
    status: 'Requested' | 'Pending' | 'Approved' | 'Rejected';
    comments: string;
    requestedAt: Date;
    respondedAt?: Date;
    respondedBy?: string;
}

@Component({
    selector     : 'approval-table',
    standalone   : true,
    templateUrl  : './approval-table.component.html',
    encapsulation: ViewEncapsulation.None,
    imports: [ReactiveFormsModule, CommonModule]
})
export class ApprovalTableComponent {
    approvals: Approval[] = [
        {
            requestedBy: 'Dave',
            requestedEmail: 'dave@example.com',
            status: 'Requested',
            comments: 'Need access to dashboard',
            requestedAt: new Date('2025-05-25T10:00:00'),
        },
        {
            requestedBy: 'Alice',
            requestedEmail: 'alice@example.com',
            status: 'Pending',
            comments: 'Need access to dashboard',
            requestedAt: new Date('2025-05-25T10:00:00'),
        },
        {
            requestedBy: 'Bob',
            requestedEmail: 'bob@example.com',
            status: 'Approved',
            comments: 'Approved for project',
            requestedAt: new Date('2025-05-24T09:30:00'),
            respondedAt: new Date('2025-05-24T12:00:00'),
            respondedBy: 'Manager'
        },
        {
            requestedBy: 'Carol',
            requestedEmail: 'carol@example.com',
            status: 'Rejected',
            comments: 'Insufficient details',
            requestedAt: new Date('2025-05-23T14:15:00'),
            respondedAt: new Date('2025-05-23T15:00:00'),
            respondedBy: 'Admin'
        }
    ];
}